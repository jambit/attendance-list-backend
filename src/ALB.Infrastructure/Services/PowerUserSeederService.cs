using ALB.Domain.Identity;
using ALB.Domain.Values;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NodaTime;

namespace ALB.Infrastructure.Services;

public class PowerUserSeederService(IServiceProvider serviceProvider) : IHostedService
{
    
    private const string AdminEmail = "admin@attendance-list-backend.de";
    private const string AdminPassword = "SoSuperSecureP4a55w0rd!";
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        
        using var scope = serviceProvider.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        
        string[] roleNames = [SystemRoles.Admin, SystemRoles.CoAdmin, SystemRoles.Team, SystemRoles.Parent];

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await roleManager.CreateAsync(new ApplicationRole
                {
                    Name = roleName,
                    Description = $"Role to assign {roleName} permissions"
                });
            }
        }
        
        var user = await userManager.FindByEmailAsync(AdminEmail);

        if (user is null)
        {
            var systemUser = new ApplicationUser
            {
                Email = AdminEmail,
                UserName = AdminEmail,
                FirstName = "Admin",
                LastName = "Admin",
                EmailConfirmed = true,
                CreatedAt = SystemClock.Instance.GetCurrentInstant()
            };
            
            var createdUser = await userManager.CreateAsync(systemUser, AdminPassword);
            
            if (createdUser.Succeeded)
                await userManager.AddToRoleAsync(systemUser, SystemRoles.Admin);
        }
    }
    
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    
}