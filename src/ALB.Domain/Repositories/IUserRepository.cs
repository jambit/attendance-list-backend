using ALB.Domain.Identity;

namespace ALB.Domain.Repositories;

public interface IUserRepository
{
    Task<ApplicationUser>CreateAsync(ApplicationUser user);
    Task<ApplicationUser?> GetByIdAsync(Guid id);
    Task<IEnumerable<ApplicationUser>> GetAllAsync();
    Task UpdateAsync(ApplicationUser user);
    Task DeleteAsync(Guid id);
}