using ALB.Domain.Enum;

namespace ALB.Domain.Entities;

public class AttendanceListEntry
{
    public Guid Id { get; set; }
    
    public DateOnly Date { get; set; }
    
    public TimeOnly? ArrivalAt { get; set; }
    
    public TimeOnly? DepartureAt { get; set; }
    
    public AttendanceStatus Status { get; set; }
    
    public Guid ChildId { get; set; }
    public virtual Child Child { get; set; }
    
    public Guid AttendanceListId { get; set; }
    public virtual AttendanceList AttendanceList { get; set; }
}