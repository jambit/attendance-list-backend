using ALB.Infrastructure.Persistence;
using ALB.Infrastructure.Persistence.Examples;
using ALB.Infrastructure.Persistence.Examples.Adapters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ALB.Infrastructure.Extensions;

public static class InfrastructureExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Caution, DbContexts arent added this way!
        services.AddSingleton<ExamplesDbContext>();
        services.AddScoped<IExamplesAdapter, ExamplesAdapter>();

        services.AddDbContextPool<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("postgresdb"), x => x.UseNodaTime());
        });
        return services;
    }
}