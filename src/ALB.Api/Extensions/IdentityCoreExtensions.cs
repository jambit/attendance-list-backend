using ALB.Domain.Identity;
using ALB.Domain.Values;
using ALB.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace ALB.Api.Extensions;

public static class IdentityCoreExtensions
{
    public static IServiceCollection AddAuthAndIdentityCore(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy(SystemRoles.AdminPolicy, x => x.RequireRole(SystemRoles.Admin))
            .AddPolicy(SystemRoles.CoAdminPolicy, x => x.RequireRole(SystemRoles.CoAdmin))
            .AddPolicy(SystemRoles.TeamPolicy, x => x.RequireRole(SystemRoles.Team))
            .AddPolicy(SystemRoles.ParentPolicy, x => x.RequireRole(SystemRoles.Parent));

        services.AddAuthentication()
            .AddCookie(IdentityConstants.ApplicationScheme)
            .AddBearerToken(IdentityConstants.BearerScheme);
        
        services.AddIdentityCore<ApplicationUser>(options =>
            {
                //TODO: Configure Identity options
            })
            .AddRoles<ApplicationRole>()
            .AddSignInManager<SignInManager<ApplicationUser>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        
        return services;
    }
}