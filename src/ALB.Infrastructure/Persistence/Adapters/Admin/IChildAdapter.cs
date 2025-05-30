using ALB.Domain.Entities;

namespace ALB.Infrastructure.Persistence.Adapters.Admin;

public interface IChildAdapter
{
    Task<Child>CreateAsync(Child child);
    Task<Child?> GetByIdAsync(Guid id);
    Task<IEnumerable<Child>> GetAllAsync();
    Task UpdateAsync(Child child);
    Task DeleteAsync(Guid id);
}