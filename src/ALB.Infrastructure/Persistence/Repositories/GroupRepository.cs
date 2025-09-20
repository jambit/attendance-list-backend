using ALB.Domain.Entities;
using ALB.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ALB.Infrastructure.Persistence.Repositories;

public class GroupRepository(ApplicationDbContext dbContext) : IGroupRepository
{
    public async Task<Group> CreateAsync(Group group, CancellationToken ct)
    {
        dbContext.Groups.Add(group);
        await dbContext.SaveChangesAsync(ct);
        return group;
    }

    public async Task<Group?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await dbContext.Groups.FindAsync([id], ct);
    }

    public async Task UpdateAsync(Group group, CancellationToken ct)
    {
        dbContext.Groups.Update(group);
        await dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var group = await dbContext.Groups.FindAsync([id], ct);
        if (group is not null)
        {
            dbContext.Groups.Remove(group);
            await dbContext.SaveChangesAsync(ct);
        }
    }

    public async Task<IEnumerable<Group>> GetAllAsync(CancellationToken ct)
    {
        return await dbContext.Groups.ToListAsync(ct);
    }

    public async Task AddChildrenToGroupAsync(Guid groupId, IEnumerable<Guid> childIds, CancellationToken ct)
    {
        var group = await dbContext.Groups
            .Include(g => g.Children)
            .FirstOrDefaultAsync(g => g.Id == groupId, ct);

        if (group is null) throw new Exception("Group not found");

        var children = await dbContext.Children
            .Where(c => childIds.Contains(c.Id))
            .ToListAsync(ct);

        foreach (var child in children)
            if (!group.Children.Contains(child))
                group.Children.Add(child);

        await dbContext.SaveChangesAsync(ct);
    }

    public async Task RemoveChildrenFromGroupAsync(Guid groupId, IEnumerable<Guid> childIds, CancellationToken ct)
    {
        var group = await dbContext.Groups
            .Include(g => g.Children)
            .FirstOrDefaultAsync(g => g.Id == groupId, ct);

        if (group is null) throw new Exception("Group not found");

        var childrenToRemove = group.Children
            .Where(c => childIds.Contains(c.Id))
            .ToList();

        foreach (var child in childrenToRemove) group.Children.Remove(child);

        await dbContext.SaveChangesAsync(ct);
    }
}