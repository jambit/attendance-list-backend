using Microsoft.AspNetCore.Identity;

namespace ALB.Domain.Identity;

public class ApplicationUserRole : IdentityUserRole<Guid>
{
    public ApplicationRole Role { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
}