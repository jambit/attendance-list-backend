using Microsoft.AspNetCore.Identity;

namespace ALB.Domain.Identity;

public class UserRole : IdentityUserRole<int>
{
    public DateTime AssignedAt { get; init; } = DateTime.Now;
    
    public virtual ApplicationRole Role { get; init; }
    public virtual ApplicationUser User { get; init; }
}