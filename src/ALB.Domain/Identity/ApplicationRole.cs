using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ALB.Domain.Identity;

public class ApplicationRole : IdentityRole
{
    [Required]
    [MaxLength(200)]
    public required string Description { get; set; }
    
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
}