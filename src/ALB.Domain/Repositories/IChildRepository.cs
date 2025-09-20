using ALB.Domain.Entities;

namespace ALB.Domain.Repositories;

public interface IChildRepository
{
    Task<Child>CreateAsync(Child child, CancellationToken ct);
    Task<Child?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IEnumerable<Child>> GetAllAsync(CancellationToken ct);
    Task UpdateAsync(Child child, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
    Task<IEnumerable<Child>> GetByCohortAsync(Guid cohortId, CancellationToken ct);

}