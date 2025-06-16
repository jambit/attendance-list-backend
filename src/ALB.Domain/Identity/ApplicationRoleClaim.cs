using Microsoft.AspNetCore.Identity;

namespace ALB.Domain.Identity;

public class ApplicationRoleClaim : IdentityRoleClaim<Guid>
{
    public ApplicationRole Role { get; set; } = null!;
}