using ALB.Domain.Entities;

namespace ALB.Infrastructure.Persistence.Repositories.Admin;

public interface IGroupRepository
{
    Task<Group> CreateAsync(Group group);
    Task<Group?> GetByIdAsync(Guid id);
    Task UpdateAsync(Group group);
    Task DeleteAsync(Guid id);
    
    Task AddChildrenToGroupAsync(Guid groupId, IEnumerable<Guid> childIds, CancellationToken ct);
    Task RemoveChildrenFromGroupAsync(Guid groupId, IEnumerable<Guid> childIds, CancellationToken ct);

}