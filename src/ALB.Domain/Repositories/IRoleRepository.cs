using System;
using System.Threading.Tasks;

namespace ALB.Domain.Repositories;

public interface IUserRoleRepository
{
    Task AssignRoleToUserAsync(Guid userId, string roleName);
    Task RemoveRoleFromUserAsync(Guid userId, string roleName);
}