using System.ComponentModel.DataAnnotations;
using ALB.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ALB.Domain.Identity;

public class ApplicationUser : IdentityUser
{
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }
    
    public DateTime CreatedAt { get; init; }
    
    public bool IsActive { get; set; } = true;
    
    public virtual ICollection<UserChildRelationship> UserChildRelationships { get; set; }
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    public virtual ICollection<UserGroup> UserGroups { get; set; }
}