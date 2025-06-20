using ALB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ALB.Domain.Repositories;


namespace ALB.Infrastructure.Persistence.Repositories;
    
public class ChildRepository(ApplicationDbContext dbContext) : IChildRepository
{
    public async Task<Child> CreateAsync(Child child)
        {
            dbContext.Children.Add(child);
            await dbContext.SaveChangesAsync();
            return child;
        }

        public async Task<Child?> GetByIdAsync(Guid id)
        {
            return await dbContext.Children.FindAsync(id);
        }

        public async Task<IEnumerable<Child>> GetAllAsync()
        {
            return await dbContext.Children.ToListAsync();
        }

        public async Task UpdateAsync(Child child)
        {
            dbContext.Children.Update(child);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var child = await dbContext.Children.FindAsync(id);
            if (child != null)
            {
                dbContext.Children.Remove(child);
                await dbContext.SaveChangesAsync();
            }
        }
        
        public async Task<IEnumerable<Child>> GetByCohortAsync(Guid cohortId)
        {
            return await dbContext.Children
                .Where(child => child.Group.Cohorts.Any(c => c.Id == cohortId))
                .ToListAsync();
        }
    }
