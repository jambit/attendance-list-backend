using Microsoft.AspNetCore.Identity;

namespace ALB.Domain.Identity;

public class ApplicationUserLogin : IdentityUserLogin<Guid>
{
    public ApplicationUser User { get; set; } = null!;
}