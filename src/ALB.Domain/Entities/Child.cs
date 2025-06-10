using System.ComponentModel.DataAnnotations;
using ALB.Domain.Identity;

namespace ALB.Domain.Entities;

public class Child
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public required string FirstName { get; set; }
    
    [Required]
    [MaxLength(50)]
    public required string LastName { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    
    public Guid GroupId { get; set; }
    
    public virtual Group Group { get; set; }
    public virtual ICollection<AttendanceListEntry> Attendances { get; set; }
    public virtual ICollection<UserChildRelationship> UserChildRelationships { get; set; }
    public virtual ICollection<AbsenceDay> AbsenceDays { get; set; }
}