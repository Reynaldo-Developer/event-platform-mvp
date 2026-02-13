using MassTransit;
using Microsoft.EntityFrameworkCore;
using NotificationService.Contracts;
using NotificationService.Data;
using NotificationService.Entities;

public class EventCreatedConsumer : IConsumer<EventCreated>
{
    private readonly NotificationDbContext _db;

    public EventCreatedConsumer(NotificationDbContext db)
    {
        _db = db;
    }

    public async Task Consume(ConsumeContext<EventCreated> ctx)
    {
        var exists = await _db.ProcessedMessages
            .AnyAsync(x => x.MessageId == ctx.Message.MessageId);

        if (exists)
        {
            Console.WriteLine("Duplicate message ignored");
            return;
        }

        Console.WriteLine($"Notify event: {ctx.Message.Name}");

        _db.ProcessedMessages.Add(new ProcessedMessage
        {
            MessageId = ctx.Message.MessageId
        });

        await _db.SaveChangesAsync();
    }
}