using System;
using System.Threading.Tasks;

namespace ALB.Infrastructure.Persistence.Repositories.Admin;

public interface IUserRoleRepository
{
    Task AssignRoleToUserAsync(Guid userId, string roleName);
    Task RemoveRoleFromUserAsync(Guid userId, string roleName);
}