using System.ComponentModel.DataAnnotations;
using ALB.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ALB.Domain.Identity;

public class ApplicationUser : IdentityUser
{
    [Required]
    [MaxLength(50)]
    public required string FirstName { get; set; }
    
    [Required]
    [MaxLength(50)]
    public required string LastName { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public bool IsActive { get; set; } = true;
    
    public virtual ICollection<UserChildRelationship> UserChildRelationships { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; }
    public virtual ICollection<UserGroup> UserGroups { get; set; }
}