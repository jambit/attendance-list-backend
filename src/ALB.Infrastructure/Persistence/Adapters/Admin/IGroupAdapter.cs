using ALB.Domain.Entities;

namespace ALB.Infrastructure.Persistence.Adapters.Admin;

public interface IGroupAdapter
{
    Task<Group> CreateAsync(Group group);
    Task<Group?> GetByIdAsync(Guid id);
    Task UpdateAsync(Group group);
    Task DeleteAsync(Guid id);
}