using ALB.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace ALB.Infrastructure.Persistence.Adapters.Admin;
    
public class ChildAdapter : IChildAdapter
    {
        private readonly ApplicationDbContext dbContext;

        public ChildAdapter(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

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
    }
