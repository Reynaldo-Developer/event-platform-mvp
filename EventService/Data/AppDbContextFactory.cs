using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EventService.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseSqlServer(
            "Server=DESKTOP-OB1CGUC;Database=EventDb;User Id=sa;Password=Marvel2026;TrustServerCertificate=True"
        );

        return new AppDbContext(optionsBuilder.Options);
    }
}