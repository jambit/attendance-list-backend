using ALB.Domain.Entities;
using ALB.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ALB.Infrastructure.Persistence.Repositories;

public class ChildRepository(ApplicationDbContext dbContext) : IChildRepository
{
    public async Task<Child> CreateAsync(Child child, CancellationToken ct)
    {
        dbContext.Children.Add(child);
        await dbContext.SaveChangesAsync(ct);
        return child;
    }

    public async Task<Child?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await dbContext.Children.FindAsync([id], ct);
    }

    public async Task<IEnumerable<Child>> GetAllAsync(CancellationToken ct)
    {
        return await dbContext.Children.ToListAsync(ct);
    }

    public async Task UpdateAsync(Child child, CancellationToken ct)
    {
        dbContext.Children.Update(child);
        await dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var child = await dbContext.Children.FindAsync([id], ct);
        if (child is not null)
        {
            dbContext.Children.Remove(child);
            await dbContext.SaveChangesAsync(ct);
        }
    }

    public async Task<IEnumerable<Child>> GetByCohortAsync(Guid cohortId, CancellationToken ct)
    {
        return await dbContext.Children
            .Include(c => c.Group).ThenInclude(g => g.Cohorts)
            .Where(child => child.Group.Cohorts.Any(c => c.Id == cohortId))
            .ToListAsync(ct);
    }
}