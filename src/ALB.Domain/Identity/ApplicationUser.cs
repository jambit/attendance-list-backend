using System.ComponentModel.DataAnnotations;
using ALB.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using NodaTime;

namespace ALB.Domain.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public Instant? CreatedAt { get; init; }
    
    public bool IsActive { get; set; } = true;
    
    public Guid? AttendanceListId { get; set; }
    
    public virtual AttendanceList? AttendanceList { get; set; }
    public virtual ICollection<Group> ResponsibleGroups { get; set; }
    public virtual ICollection<Group> SupervisedGroups { get; set; }
    public virtual ICollection<UserChildRelationship> UserChildRelationships { get; set; }
    public virtual ICollection<UserGroup> UserGroups { get; set; }
    public virtual ICollection<ApplicationUserClaim> Claims { get; set; }
    public virtual ICollection<ApplicationUserLogin> Logins { get; set; }
    public virtual ICollection<ApplicationUserToken> Tokens { get; set; }
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
}