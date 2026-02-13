using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NotificationService.Data;

public class NotificationDbContextFactory
    : IDesignTimeDbContextFactory<NotificationDbContext>
{
    public NotificationDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<NotificationDbContext>()
            .UseSqlServer("Server=DESKTOP-OB1CGUC;Database=EventDb;User Id=sa;Password=Marvel2026;TrustServerCertificate=True")
            .Options;

        return new NotificationDbContext(options);
    }
}