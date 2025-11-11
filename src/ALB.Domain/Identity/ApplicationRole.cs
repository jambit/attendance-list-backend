using Microsoft.AspNetCore.Identity;

namespace ALB.Domain.Identity;

public class ApplicationRole : IdentityRole<Guid>
{
    public required string Description { get; set; }

    public ICollection<ApplicationRoleClaim> RoleClaims { get; set; } = new List<ApplicationRoleClaim>();
    public ICollection<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>();
}