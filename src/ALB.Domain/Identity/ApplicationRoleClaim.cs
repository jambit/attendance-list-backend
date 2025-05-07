using Microsoft.AspNetCore.Identity;

namespace ALB.Domain.Identity;

public class ApplicationRoleClaim : IdentityRoleClaim<Guid>
{
    public virtual ApplicationRole Role { get; set; }
}