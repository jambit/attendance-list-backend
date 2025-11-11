using ALB.Domain.Entities;

namespace ALB.Domain.Repositories;

public interface IChildRepository
{
    Task<Child> CreateAsync(Child child);
    Task<Child?> GetByIdAsync(Guid id);
    Task<IEnumerable<Child>> GetAllAsync();
    Task UpdateAsync(Child child);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Child>> GetByCohortAsync(Guid cohortId);

}