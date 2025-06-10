using ALB.Domain.Identity;

namespace ALB.Domain.Entities;

public class AttendanceList
{
    public Guid Id { get; set; }
    
    public virtual ICollection<ApplicationUser> Writers { get; set; } = new List<ApplicationUser>();
    
    public bool Open { get; set; } = true;
    
    public virtual ICollection<AttendanceListEntry> Entries { get; set; } = new List<AttendanceListEntry>();
    
    public Guid GradeId { get; set; }
    public virtual Grade Grade { get; set; }
}