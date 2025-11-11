using System.Security.Claims;

using ALB.Api.Endpoints;
using ALB.Api.Exceptions;
using ALB.Api.Extensions;
using ALB.Domain.Identity;
using ALB.Domain.Values;
using ALB.Infrastructure.Authentication;
using ALB.Infrastructure.Extensions;
using ALB.Infrastructure.Persistence;

using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;

using Npgsql;

using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddNpgsqlDataSource("postgresdb",
    configureDataSourceBuilder: sourceBuilder => sourceBuilder.UseNodaTime());

builder.AddServiceDefaults();

// This is needed to make OpenApi forwarding possible behind aspires reverse proxy.
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

// TODO: secret to azure fault
builder.Services
    .AddOptions<JwtOptions>()
    .Bind(builder.Configuration.GetSection(JwtOptions.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Instance =
            $"{context.HttpContext.Request.Method}{context.HttpContext.Request.Path}";

        context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

        var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
        context.ProblemDetails.Extensions.TryAdd("traceId", activity?.TraceId);
    };
});

builder.Services.AddExceptionHandler<ProblemExceptionHandler>();

builder.Services.AddOpenApi();

//TODO: Implement new EmailSender and remove DummyEmailSender
builder.Services.AddTransient<IEmailSender<ApplicationUser>, DummyEmailSender>();

builder.Services.AddNodaTimeJsonConverters();

builder.Services.AddAuthAndIdentityCore(builder.Configuration);


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

app.MapEndpoints();
app.MapGet("/me", (ClaimsPrincipal claims) => Results.Ok(claims.Claims.ToDictionary(c => c.Type, c => c.Value)))
    .WithOpenApi()
    .RequireAuthorization(policy => policy.RequireRole(SystemRoles.Admin));

app.MapIdentityApiFilterable<ApplicationUser>(new IdentityApiEndpointRouteBuilderOptions
{
    ExcludeLoginPost = true,
    ExcludeRefreshPost = true,
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