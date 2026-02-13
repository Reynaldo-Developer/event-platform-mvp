using Microsoft.EntityFrameworkCore;
using NotificationService.Data;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<NotificationDbContext>(opt =>
    opt.UseSqlServer(
        "Server=DESKTOP-OB1CGUC;Database=EventDb;User Id=sa;Password=Marvel2026;TrustServerCertificate=True"));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<EventCreatedConsumer>();

    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("rabbitmq");

        cfg.ReceiveEndpoint("event-created-queue", e =>
        {
            e.ConfigureConsumer<EventCreatedConsumer>(ctx);
        });
    });
});

var host = builder.Build();
host.Run();