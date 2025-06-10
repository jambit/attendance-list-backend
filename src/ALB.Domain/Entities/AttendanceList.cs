using ALB.Domain.Identity;

namespace ALB.Domain.Entities;

public class AttendanceList
{
    public Guid Id { get; set; }
    
    public bool Open { get; set; } = true;
    
    public Guid GradeId { get; set; }
    public virtual Grade Grade { get; set; }
    public virtual ICollection<ApplicationUser> Writers { get; set; } = new List<ApplicationUser>();
    public virtual ICollection<AttendanceListEntry> AttendanceListEntries { get; set; } = new List<AttendanceListEntry>();
}