using Microsoft.EntityFrameworkCore;
using NotificationService.Entities;

namespace NotificationService.Data;

public class NotificationDbContext : DbContext
{
    public DbSet<ProcessedMessage> ProcessedMessages => Set<ProcessedMessage>();

    public NotificationDbContext(DbContextOptions<NotificationDbContext> options)
        : base(options) { }
}