using ALB.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace ALB.Infrastructure.Persistence.Adapters.Admin;

public class UserRoleAdapter : IUserRoleAdapter
{
    private readonly ApplicationDbContext dbContext;
    private readonly UserManager<ApplicationUser> userManager;

    public UserRoleAdapter(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        this.dbContext = dbContext;
        this.userManager = userManager;
    }

    public async Task AssignRoleToUserAsync(Guid userId, Guid roleId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            throw new Exception($"User with ID {userId} not found.");

        var role = await dbContext.Roles.FindAsync(roleId);
        if (role == null)
            throw new Exception($"Role with ID {roleId} not found.");
        
        if (!await userManager.IsInRoleAsync(user, role.Name))
        {
            await userManager.AddToRoleAsync(user, role.Name);
        }
    }

    public async Task RemoveRoleFromUserAsync(Guid userId, Guid roleId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            throw new Exception($"User with ID {userId} not found.");

        var role = await dbContext.Roles.FindAsync(roleId);
        if (role == null)
            throw new Exception($"Role with ID {roleId} not found.");
        
        if (await userManager.IsInRoleAsync(user, role.Name))
        {
            await userManager.RemoveFromRoleAsync(user, role.Name);
        }
    }
}