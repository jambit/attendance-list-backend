using ALB.Api;
using ALB.Api.Extensions;
using ALB.Domain.Identity;
using ALB.Domain.Values;
using ALB.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Testcontainers.PostgreSql;
using TUnit.Core.Interfaces;

namespace ApiIntegrationTests;

public class BaseIntegrationTest : IAsyncInitializer, IAsyncDisposable
{
    public const string AdminEmail = "admin@attendance-list-backend.de";
    public const string AdminPassword = "SoSuperSecureP4a55w0rd!";
    
    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
        .WithUsername("postgres")
        .WithPassword("postgres")
        .WithDatabase("postgres")
        .Build();

    private WebApplicationFactory<IApiAssemblyMarker> _webApplicationFactory = default!;

    private RoleManager<ApplicationRole> _roleManager = default!;
    private UserManager<ApplicationUser> _userManager = default!;
    
    private HttpClient GetHttpClient(string token)
    {
        var client = _webApplicationFactory.CreateClient();

        client.DefaultRequestHeaders.Add(
            "X-Api-Key",
            token
        );

        return client;
    }

    public HttpClient GetAdminClient()
        => GetHttpClient("Admin");
    
    
    public async Task InitializeAsync()
    {
        await _postgreSqlContainer.StartAsync();

        _webApplicationFactory = new TestWebApplicationFactory(_postgreSqlContainer.GetConnectionString());
        
        _ = _webApplicationFactory.Server;
        
        using var serviceScope = _webApplicationFactory.Services.CreateScope();

        var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        // await context.Database.MigrateAsync();
        await context.Database.EnsureCreatedAsync();
        
        _roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        _userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        await SeedDatabase();
    }
    
    private async Task SeedDatabase()
    {
        string[] roleNames = [SystemRoles.Admin, SystemRoles.CoAdmin, SystemRoles.Team, SystemRoles.Parent];

        foreach (var roleName in roleNames)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await _roleManager.CreateAsync(new ApplicationRole
                {
                    Name = roleName,
                    Description = $"Role to assign {roleName} permissions"
                });
            }
        }
        
        var user = await _userManager.FindByEmailAsync(AdminEmail);

        if (user is null)
        {
            var systemUser = new ApplicationUser
            {
                Email = AdminEmail,
                UserName = AdminEmail,
                FirstName = "Admin",
                LastName = "Admin",
                EmailConfirmed = true
            };
            
            var createdUser = await _userManager.CreateAsync(systemUser, AdminPassword);
            
            if (createdUser.Succeeded)
                await _userManager.AddToRoleAsync(systemUser, SystemRoles.Admin);
        }
    }
    
    public async ValueTask DisposeAsync()
    {
        await _webApplicationFactory.DisposeAsync();
        await _postgreSqlContainer.StopAsync();
    }
}

file sealed class TestWebApplicationFactory(string connectionString) : WebApplicationFactory<IApiAssemblyMarker>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        _ = builder.UseEnvironment("Testing");

        _ = builder.ConfigureHostConfiguration(
            cb => cb.AddInMemoryCollection(
                new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase)
                {
                    // ["UseSecretsJson"] = bool.FalseString,
                    // ["UseAuth0"] = bool.FalseString,
                    // ["ProcessFeatureJob:Enabled"] = bool.FalseString,
                    [$"ConnectionStrings:postgresdb"] = connectionString
                }
            )
        );

        builder.ConfigureServices(services =>
        {
            var desc = services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

            if (desc is not null)
            {
                services.Remove(desc);
            }

            services.AddDbContext<ApplicationDbContext>(opts =>
            {
                opts.UseNpgsql(connectionString, x => x.UseNodaTime());
            });
        });

        return base.CreateHost(builder);
    }
}