using ALB.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ALB.Infrastructure.Persistence.Repositories.Admin;

public class GroupRepository : IGroupRepository
{
    private readonly ApplicationDbContext dbContext;

    public GroupRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Group> CreateAsync(Group group)
    {
        dbContext.Groups.Add(group);
        await dbContext.SaveChangesAsync();
        return group;
    }
    public async Task<Group?> GetByIdAsync(Guid id)
    {
        return await dbContext.Groups.FindAsync(id);
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
    
    public async Task AddChildrenToGroupAsync(Guid groupId, IEnumerable<Guid> childIds, CancellationToken ct)
    {
        var group = await dbContext.Groups
            .Include(g => g.Children)
            .FirstOrDefaultAsync(g => g.Id == groupId, ct);

        if (group == null) throw new Exception("Group not found");

        var children = await dbContext.Children
            .Where(c => childIds.Contains(c.Id))
            .ToListAsync(ct);

        foreach (var child in children)
        {
            if (!group.Children.Contains(child))
                group.Children.Add(child);
        }

        await dbContext.SaveChangesAsync(ct);
    }

    public async Task RemoveChildrenFromGroupAsync(Guid groupId, IEnumerable<Guid> childIds, CancellationToken ct)
    {
        var group = await dbContext.Groups
            .Include(g => g.Children)
            .FirstOrDefaultAsync(g => g.Id == groupId, ct);

        if (group == null) throw new Exception("Group not found");

        var childrenToRemove = group.Children
            .Where(c => childIds.Contains(c.Id))
            .ToList();

        foreach (var child in childrenToRemove)
        {
            group.Children.Remove(child);
        }

        await dbContext.SaveChangesAsync(ct);
    }

}