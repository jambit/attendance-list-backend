using ALB.Api.Extensions;
using ALB.Domain.Identity;
using ALB.Infrastructure.Extensions;
using ALB.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;
using ALB.Infrastructure.Persistence.Repositories.Admin;
using ALB.Infrastructure.Persistence.Repositories.TeamMember;

var builder = WebApplication.CreateBuilder(args);

builder.AddNpgsqlDataSource(connectionName: "postgresdb");
// builder.AddNpgsqlDbContext<ApplicationDbContext>("postgresdb");

builder.AddServiceDefaults();

// This is needed to make OpenApi forwarding possible behind aspires reverse proxy.
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IChildRepository, ChildRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();


builder.Services.AddOpenApi();

//TODO: Implement new EmailSender and remove DummyEmailSender
builder.Services.AddTransient<IEmailSender<ApplicationUser>, DummyEmailSender>();

builder.Services.AddFastEndpoints();

builder.Services.AddAuthAndIdentityCore();


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
app.UseExceptionHandler("/Error");
app.UseForwardedHeaders();
app.MapOpenApi();
app.MapScalarApiReference("/api-reference", options => 
{
    options.WithTitle("Saga Demo API")
        .WithTheme(ScalarTheme.Moon)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
});

app.UseFastEndpoints();
app.MapIdentityApi<ApplicationUser>();

// TODO: add migrations when out of dev cycle
using var serviceScope = app.Services.CreateScope();
var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
await context.Database.EnsureCreatedAsync();

app.Run();