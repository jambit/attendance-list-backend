using Microsoft.AspNetCore.Identity;

namespace ALB.Domain.Identity;

public class ApplicationUserClaim : IdentityUserClaim<Guid>
{
    public ApplicationUser User { get; set; } = null!;
}