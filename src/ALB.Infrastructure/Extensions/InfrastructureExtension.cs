using ALB.Domain.Repositories;
using ALB.Infrastructure.Persistence;
using ALB.Infrastructure.Persistence.Repositories;
using ALB.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace ALB.Infrastructure.Extensions;

public static class InfrastructureExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<PowerUserSeederService>();
        
        services.AddScoped<IChildRepository, ChildRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<IAttendanceRepository, AttendanceRepository>();
        services.AddScoped<IAbsenceDayRepository, AbsenceDayRepository>();
        services.AddScoped<ICohortRepository, CohortRepository>();

        services.AddDbContextPool<ApplicationDbContext>((serviceProvider, options) =>
        {
            var dataSource = serviceProvider.GetRequiredService<NpgsqlDataSource>();
            options.UseNpgsql(dataSource, npgsqlOptions => 
                npgsqlOptions.UseNodaTime());
        });
        
        return services;
    }
}