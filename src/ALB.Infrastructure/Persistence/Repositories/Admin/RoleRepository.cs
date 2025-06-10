using ALB.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace ALB.Infrastructure.Persistence.Repositories.Admin;

public class UserRoleRepository(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
    : IUserRoleRepository
{
    public async Task AssignRoleToUserAsync(Guid userId, string roleName)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            throw new Exception($"User with ID {userId} not found.");

        var role = await dbContext.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        if (role == null)
            throw new Exception($"Role with name '{roleName}' not found.");

        if (!await userManager.IsInRoleAsync(user, role.Name))
        {
            await userManager.AddToRoleAsync(user, role.Name);
        }
    }


    public async Task RemoveRoleFromUserAsync(Guid userId, string roleName)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            throw new KeyNotFoundException($"User with ID {userId} not found.");

        if (!await userManager.IsInRoleAsync(user, roleName))
            return; 

        var result = await userManager.RemoveFromRoleAsync(user, roleName);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Failed to remove role '{roleName}': {errors}");
        }
    }


}