using ALB.Infrastructure.Persistence;
using ALB.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ALB.Infrastructure.Extensions;

public static class InfrastructureExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<PowerUserSeederService>();

        services.AddDbContextPool<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("postgresdb"), x => x.UseNodaTime());
        });
        return services;
    }
}