using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ALB.Infrastructure.Persistence;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

        builder.UseNpgsql("Host=localhost;Database=alb;Username=postgres;Password=postgres",
            npgsqlOptions => npgsqlOptions.UseNodaTime());

        return new ApplicationDbContext(builder.Options);
    }
}