using ALB.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace ALB.Infrastructure.Persistence.Repositories.Admin;

public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
{
    public async Task<ApplicationUser> CreateAsync(ApplicationUser user)
    {
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<ApplicationUser?> GetByIdAsync(Guid id)
    {
        return await dbContext.Users.FindAsync(id);
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
    {
        return await dbContext.Users.ToListAsync();
    }

    public async Task UpdateAsync(ApplicationUser user)
    {
        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await dbContext.Users.FindAsync(id);
        if (user != null)
        {
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
        }
    }
}