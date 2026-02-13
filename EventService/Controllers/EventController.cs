using EventService.Contracts;
using EventService.Data;
using EventService.Entities;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Text.Json;

[ApiController]
[Route("events")]
public class EventController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IPublishEndpoint _publish;
    private readonly IConnectionMultiplexer _redis;

    public EventController(
        AppDbContext db,
        IPublishEndpoint publish,
        IConnectionMultiplexer redis)
    {
        _db = db;
        _publish = publish;
        _redis = redis;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEventRequest request)
    {
        // 1️⃣ iniciar transacción
        using var transaction = await _db.Database.BeginTransactionAsync();

        try
        {
            // 2️⃣ mapear DTO → entidad
            var eventEntity = new EventEntity
            {
                Name = request.Name,
                Date = request.Date,
                Place = request.Place,
                Zones = request.Zones.Select(z => new ZoneEntity
                {
                    Name = z.Name,
                    Price = z.Price,
                    Capacity = z.Capacity
                }).ToList()
            };

            // 3️⃣ guardar evento + zonas (1 sola transacción)
            _db.Events.Add(eventEntity);
            await _db.SaveChangesAsync();

            // 4️⃣ commit BD
            await transaction.CommitAsync();

            // 5️⃣ publicar evento (async)    
            var message = new EventCreated(
                Guid.NewGuid(),          // MessageId
                eventEntity.Id,          // EventId
                eventEntity.Name,        // Name
                DateTime.UtcNow,         // OccurredAt
                Guid.NewGuid(),          // CorrelationId
                1                        // Version
            );

            await _publish.Publish(message);

            return Ok(new { eventEntity.Id });
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var cache = _redis.GetDatabase();
        var cached = await cache.StringGetAsync("events");

        if (!cached.IsNullOrEmpty)
            return Ok(JsonSerializer.Deserialize<object>(cached!));

        var events = await _db.Events
            .Include(e => e.Zones)
            .ToListAsync();

        await cache.StringSetAsync(
            "events",
            JsonSerializer.Serialize(events),
            TimeSpan.FromMinutes(1)
        );

        return Ok(events);
    }
}