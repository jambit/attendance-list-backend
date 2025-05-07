using Microsoft.AspNetCore.Identity;

namespace ALB.Domain.Identity;

public class ApplicationUserRole : IdentityUserRole<Guid>
{
    //public DateTime AssignedAt { get; init; } = DateTime.Now;
    
    public virtual ApplicationRole Role { get; init; }
    public virtual ApplicationUser User { get; init; }
}