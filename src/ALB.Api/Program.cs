using ALB.Api.Extensions;
using ALB.Domain.Identity;
using ALB.Domain.Repositories;
using ALB.Infrastructure.Authentication;
using ALB.Infrastructure.Extensions;
using ALB.Infrastructure.Persistence;
using ALB.Infrastructure.Persistence.Repositories;
using FastEndpoints;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Npgsql;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddNpgsqlDataSource("postgresdb",
    configureDataSourceBuilder: sourceBuilder => sourceBuilder.UseNodaTime());
// builder.AddNpgsqlDbContext<ApplicationDbContext>("postgresdb");

builder.AddServiceDefaults();

// This is needed to make OpenApi forwarding possible behind aspires reverse proxy.
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<IChildRepository, ChildRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddScoped<IAbsenceDayRepository, AbsenceDayRepository>();
builder.Services.AddScoped<ICohortRepository, CohortRepository>();

builder.Services.AddOpenApi();

//TODO: Implement new EmailSender and remove DummyEmailSender
builder.Services.AddTransient<IEmailSender<ApplicationUser>, DummyEmailSender>();

builder.Services.AddFastEndpoints();

builder.Services.AddAuthAndIdentityCore();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler("/Error");
app.UseForwardedHeaders();
app.UseAuthentication();
app.UseAuthorization();
app.MapOpenApi();
app.MapScalarApiReference("/api-reference", options =>
{
    options.WithTitle("ALB API")
        .WithTheme(ScalarTheme.Moon)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
});

app.UseFastEndpoints();
app.MapIdentityApiFilterable<ApplicationUser>(new IdentityApiEndpointRouteBuilderOptions
{
    ExcludeLoginPost = false,
    ExcludeRefreshPost = false,
    ExcludeConfirmEmailGet = false,
    ExcludeResendConfirmationEmailPost = false,
    ExcludeForgotPasswordPost = false,
    ExcludeResetPasswordPost = false,
    ExcludeRegisterPost = true,
    // Excluding ManageGroup hides 2FAPost, InfoGet and InfoPost
    ExcludeManageGroup = true,
    Exclude2FaPost = true,
    ExcludeInfoGet = true,
    ExcludeInfoPost = true
});

// TODO: add migrations when out of dev cycle
using var serviceScope = app.Services.CreateScope();
var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
await context.Database.EnsureCreatedAsync();

app.Run();