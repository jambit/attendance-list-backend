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
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                options.User.AllowedUserNameCharacters = 
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddRoles<ApplicationRole>()
            .AddSignInManager<SignInManager<ApplicationUser>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        
        return services;
    }
}