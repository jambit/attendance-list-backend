using System.ComponentModel.DataAnnotations;

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
    public virtual ICollection<Attendance> Attendances { get; set; }
    public virtual ICollection<UserChildRelationship> UserChildRelationships { get; set; }
}