using Microsoft.AspNetCore.Identity;

namespace ALB.Domain.Identity;

public class ApplicationUserToken: IdentityUserToken<Guid>
{
    public virtual ApplicationUser User { get; set; }
}