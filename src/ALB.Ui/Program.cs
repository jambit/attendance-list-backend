using ALB.Application;
using ALB.Domain.Identity;
using ALB.Infrastructure.Extensions;
using ALB.Ui.Components;
using ALB.Ui.Components.Account;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

using MudBlazor.Services;

using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.AddNpgsqlDataSource("postgresdb",
    configureDataSourceBuilder: sourceBuilder => sourceBuilder.UseNodaTime());

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, DummyEmailSender>();

builder.Services.AddNodaTimeJsonConverters();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services
    .AddApplicationServices()
    .AddAuthAndIdentityCore(builder.Configuration, false);

builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler("/Error", createScopeForErrors: true);

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();