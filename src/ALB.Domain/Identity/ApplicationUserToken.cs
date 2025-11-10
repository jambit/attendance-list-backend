using Microsoft.AspNetCore.Identity;

namespace ALB.Domain.Identity;

public class ApplicationUserToken: IdentityUserToken<Guid>
{
    public Guid Id { get; set; }
    public DateTime ExpiresOnUtc { get; set; }
    
    public ApplicationUser User { get; set; } = null!;
}