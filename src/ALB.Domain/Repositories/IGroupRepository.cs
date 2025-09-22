using ALB.Domain.Entities;

namespace ALB.Domain.Repositories;

public interface IGroupRepository
{
    Task<Group> CreateAsync(Group group, CancellationToken ct);
    Task<Group?> GetByIdAsync(Guid id, CancellationToken ct);
    Task UpdateAsync(Group group, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
    Task<IEnumerable<Group>> GetAllAsync(CancellationToken ct);
    Task AddChildrenToGroupAsync(Guid groupId, IEnumerable<Guid> childIds, CancellationToken ct);
    Task RemoveChildrenFromGroupAsync(Guid groupId, IEnumerable<Guid> childIds, CancellationToken ct);
    Task<IEnumerable<Cohort>> GetAllCohortsOfGroupAsync(Guid groupId, CancellationToken ct);
}