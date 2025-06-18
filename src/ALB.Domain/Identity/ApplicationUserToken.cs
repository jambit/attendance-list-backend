using Microsoft.AspNetCore.Identity;

namespace ALB.Domain.Identity;

public class ApplicationUserToken: IdentityUserToken<Guid>
{
    public ApplicationUser User { get; set; } = null!;
}