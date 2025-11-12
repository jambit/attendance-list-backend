using ALB.Domain.Repositories;
using ALB.Domain.Values;
using ALB.Infrastructure.Persistence;
using ALB.Infrastructure.Persistence.Repositories;
using ALB.Infrastructure.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Npgsql;

using TickerQ.Dashboard.DependencyInjection;
using TickerQ.DependencyInjection;
using TickerQ.EntityFrameworkCore.DependencyInjection;

namespace ALB.Infrastructure.Extensions;

public static class InfrastructureExtension
{
    public static IServiceCollection AddCronJobs(this IServiceCollection services)
    {
        services.AddTickerQ(options =>
        {
            options.AddOperationalStore<ApplicationDbContext>(efOptions =>
            {
                efOptions.UseModelCustomizerForMigrations();
                efOptions.CancelMissedTickersOnAppStart();
            });

            options.SetInstanceIdentifier("TickerQ");

            options.AddDashboard(dashboardOptions =>
            {
                dashboardOptions.EnableBuiltInAuth = true;
                dashboardOptions.EnableBasicAuth = true;
            });
        });
        
        return services;
    }
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<PowerUserSeederService>();

        services.AddScoped<IChildRepository, ChildRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<IAttendanceRepository, AttendanceRepository>();
        services.AddScoped<IAbsenceDayRepository, AbsenceDayRepository>();
        services.AddScoped<ICohortRepository, CohortRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        services.AddDbContextPool<ApplicationDbContext>((serviceProvider, options) =>
        {
            var dataSource = serviceProvider.GetRequiredService<NpgsqlDataSource>();
            options.UseNpgsql(dataSource, npgsqlOptions =>
                npgsqlOptions.UseNodaTime());
        });

        return services;
    }
}