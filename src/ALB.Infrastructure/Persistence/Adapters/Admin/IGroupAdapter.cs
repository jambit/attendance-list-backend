using ALB.Domain.Entities;

namespace ALB.Infrastructure.Persistence.Adapters.Admin;

public interface IGroupAdapter
{
    Task<Group> CreateAsync(Group group);
    Task UpdateAsync(Group group);
    Task DeleteAsync(Guid id);
}