using ALB.Api.Extensions;
using ALB.Api.UseCases.ExampleFeatures.Endpoints;
using ALB.Infrastructure.Extensions;
using ALB.Domain.Identity;
using ALB.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

app.MapExampleEndpoints();
app.MapIdentityApi<ApplicationUser>();

app.Run();