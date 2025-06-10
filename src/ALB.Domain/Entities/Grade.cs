namespace ALB.Domain.Entities;

public class Grade
{
    public Guid Id { get; set; }
    
    public Guid GroupId { get; set; }
    public virtual Group Group { get; set; }
    
    public virtual ICollection<AttendanceList> AttendanceLists { get; set; } = new List<AttendanceList>();
    
    public Guid LevelId { get; set; }
    public virtual Level Level { get; set; }
    
    public int CreationYear { get; set; }
}
