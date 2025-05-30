using ALB.Domain.Identity;

namespace ALB.Infrastructure.Persistence.Adapters.Admin;

public interface IUserAdapter
{
    Task<ApplicationUser>CreateAsync(ApplicationUser user);
    Task<ApplicationUser?> GetByIdAsync(Guid id);
    Task<IEnumerable<ApplicationUser>> GetAllAsync();
    Task UpdateAsync(ApplicationUser user);
    Task DeleteAsync(Guid id);
}