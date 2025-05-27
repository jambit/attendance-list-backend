using ALB.Domain.Entities;

namespace ALB.Infrastructure.Persistence.Adapters.Admin;

public class GroupAdapter : IGroupAdapter
{
    private readonly ApplicationDbContext dbContext;

    public GroupAdapter(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Group> CreateAsync(Group group)
    {
        dbContext.Groups.Add(group);
        await dbContext.SaveChangesAsync();
        return group;
    }

    public async Task UpdateAsync(Group group)
    {
        dbContext.Groups.Update(group);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var group = await dbContext.Groups.FindAsync(id);
        if (group != null)
        {
            dbContext.Groups.Remove(group);
            await dbContext.SaveChangesAsync();
        }
    }
}