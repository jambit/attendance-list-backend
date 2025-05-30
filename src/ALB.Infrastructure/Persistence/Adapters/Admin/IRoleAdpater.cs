using System;
using System.Threading.Tasks;

namespace ALB.Infrastructure.Persistence.Adapters.Admin;

public interface IUserRoleAdapter
{
    Task AssignRoleToUserAsync(Guid userId, Guid roleId);
    Task RemoveRoleFromUserAsync(Guid userId, Guid roleId);
}