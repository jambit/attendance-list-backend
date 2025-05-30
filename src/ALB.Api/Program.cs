using ALB.Api.Extensions;
using ALB.Domain.Identity;
using ALB.Infrastructure.Authentication;
using ALB.Infrastructure.Extensions;
using ALB.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;

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
app.MapIdentityApiFilterable<ApplicationUser>(new IdentityApiEndpointRouteBuilderOptions()
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
    ExcludeInfoPost = true,
});

// TODO: add migrations when out of dev cycle
using var serviceScope = app.Services.CreateScope();
var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
await context.Database.EnsureCreatedAsync();

app.Run();