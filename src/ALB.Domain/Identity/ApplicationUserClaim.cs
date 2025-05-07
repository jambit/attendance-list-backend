using Microsoft.AspNetCore.Identity;

namespace ALB.Domain.Identity;

public class ApplicationUserClaim : IdentityUserClaim<Guid>
{
    public virtual ApplicationUser User { get; set; }
}