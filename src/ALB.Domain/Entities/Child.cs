namespace ALB.Domain.Entities;

public class Child
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Guid GroupId { get; set; }
    
    public Group Group { get; set; } = null!;
    public ICollection<AttendanceListEntry> AttendanceListEntries { get; set; } = new List<AttendanceListEntry>();
    public ICollection<UserChildRelationship> UserChildRelationships { get; set; } = new List<UserChildRelationship>();
    public ICollection<AbsenceDay> AbsenceDays { get; set; } = new List<AbsenceDay>();
}