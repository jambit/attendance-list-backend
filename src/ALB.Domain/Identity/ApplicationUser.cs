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
    
    public ICollection<Group> ResponsibleGroups { get; set; } = new List<Group>();
    public ICollection<UserChildRelationship> UserChildRelationships { get; set; } = new List<UserChildRelationship>();
    public ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
    public ICollection<ApplicationUserClaim> Claims { get; set; } = new List<ApplicationUserClaim>();
    public ICollection<ApplicationUserLogin> Logins { get; set; } = new List<ApplicationUserLogin>();
    public ICollection<ApplicationUserToken> Tokens { get; set; } = new List<ApplicationUserToken>();
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public ICollection<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>();
    public ICollection<AttendanceListWriter> WriterAssignments { get; set; } = new List<AttendanceListWriter>();
}