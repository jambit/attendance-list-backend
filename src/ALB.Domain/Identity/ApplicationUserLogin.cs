using Microsoft.AspNetCore.Identity;

namespace ALB.Domain.Identity;

public class ApplicationUserLogin : IdentityUserLogin<Guid>
{
    public virtual ApplicationUser User { get; set; }
}