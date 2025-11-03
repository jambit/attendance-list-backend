using System.Security.Claims;
using System.Text.Encodings.Web;
using ALB.Api;
using ALB.Domain.Values;
using ALB.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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

    private WebApplicationFactory<IApiAssemblyMarker> _webApplicationFactory = null!;

    public async ValueTask DisposeAsync()
    {
        await _webApplicationFactory.DisposeAsync();
        await _postgreSqlContainer.StopAsync();
    }

    public async Task InitializeAsync()
    {
        await _postgreSqlContainer.StartAsync();

        _webApplicationFactory = new TestWebApplicationFactory(_postgreSqlContainer.GetConnectionString());

        _ = _webApplicationFactory.Server;

        using var serviceScope = _webApplicationFactory.Services.CreateScope();

        var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        // await context.Database.MigrateAsync();
        await context.Database.EnsureCreatedAsync();
    }

    private HttpClient GetHttpClient(string token)
    {
        var client = _webApplicationFactory.CreateClient();

        client.DefaultRequestHeaders.Add(
            "X-Api-Key",
            token
        );

        return client;
    }

    public IServiceScope GetScope()
    {
        return _webApplicationFactory.Services.CreateScope();
    }

    public HttpClient GetAdminClient()
    {
        return GetHttpClient(SystemRoles.Admin);
    }

    public HttpClient GetCoAdminClient()
    {
        return GetHttpClient(SystemRoles.CoAdmin);
    }

    public HttpClient GetTeamClient()
    {
        return GetHttpClient(SystemRoles.Team);
    }

    public HttpClient GetParentClient()
    {
        return GetHttpClient(SystemRoles.Parent);
    }
}

internal class TestAuthHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var apiKey = Request.Headers["X-Api-Key"].ToString();

        var claims = apiKey switch
        {
            SystemRoles.Admin => new[]
            {
                new Claim(ClaimTypes.Name, "AdminUser"),
                new Claim(ClaimTypes.Role, "Admin")
            },
            SystemRoles.Parent => new[]
            {
                new Claim(ClaimTypes.Name, "ParentUser"),
                new Claim(ClaimTypes.Role, "Parent")
            },
            _ => throw new InvalidOperationException("Unknown API Key")
        };

        var identity = new ClaimsIdentity(claims, "Test");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "TestScheme");

        var result = AuthenticateResult.Success(ticket);

        return Task.FromResult(result);
    }
}

file sealed class TestWebApplicationFactory(string connectionString)
    : WebApplicationFactory<IApiAssemblyMarker>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, configBuilder) =>
        {
            var inMemorySettings = new Dictionary<string, string?>
            {
                { "ConnectionStrings:DefaultConnection", connectionString }
            };
            configBuilder.AddInMemoryCollection(inMemorySettings);
        });

        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor =
                services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

            if (dbContextDescriptor is not null)
                services.Remove(dbContextDescriptor);

            services.AddDbContext<ApplicationDbContext>((container, options) =>
            {
                options.UseNpgsql(connectionString, npgsqlOptions => npgsqlOptions.UseNodaTime());
            });

            services.AddAuthentication("TestScheme")
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("TestScheme", options => { });

            services.AddAuthorizationBuilder()
                .AddPolicy(SystemRoles.AdminPolicy, x => x.RequireRole(SystemRoles.Admin))
                .AddPolicy(SystemRoles.CoAdminPolicy, x => x.RequireRole(SystemRoles.CoAdmin))
                .AddPolicy(SystemRoles.TeamPolicy, x => x.RequireRole(SystemRoles.Team))
                .AddPolicy(SystemRoles.ParentPolicy, x => x.RequireRole(SystemRoles.Parent));
        });
        builder.UseEnvironment("Testing");
    }
}