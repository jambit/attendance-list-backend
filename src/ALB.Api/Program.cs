using ALB.Api.UseCases.ExampleFeatures.Endpoints;
using ALB.Infrastructure.Extensions;
using ALB.Infrastructure.Identity;
using ALB.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// This is needed to make OpenApi forwarding possible behind aspires reverse proxy.
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

builder.Services.AddInfrastructure();

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AlbDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("postgresdb")));

builder.Services.AddIdentityCore<AlbUser>(options =>
{
    //TODO: Configure Identity options
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<AlbDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    //TODO: Configure Authentication options
});

builder.Services.AddAuthorizationBuilder()
    //TODO: Configure Authorization policies
    .SetDefaultPolicy(new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build());


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

app.MapExampleEndpoints();

app.Run();