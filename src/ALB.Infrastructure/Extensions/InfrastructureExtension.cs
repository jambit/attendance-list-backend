using ALB.Infrastructure.Persistence.Examples;
using ALB.Infrastructure.Persistence.Examples.Adapters;
using Microsoft.Extensions.DependencyInjection;

namespace ALB.Infrastructure.Extensions;

public static class InfrastructureExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Caution, DbContexts arent added this way!
        services.AddSingleton<ExamplesDbContext>();
        services.AddScoped<IExamplesAdapter, ExamplesAdapter>();
        return services;
    }
}