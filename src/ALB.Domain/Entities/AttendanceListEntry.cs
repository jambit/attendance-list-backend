using ALB.Domain.Enum;

namespace ALB.Domain.Entities;

public class AttendanceListEntry
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly? ArrivalAt { get; set; }
    public TimeOnly? DepartureAt { get; set; }
    public Guid AttendanceListId { get; set; }
    public int AttendanceStatusId { get; set; }
    public Guid ChildId { get; set; }
    
    public AttendanceList AttendanceList { get; set; } = null!;
    public AttendanceStatus AttendanceStatus { get; set; } = null!;
    public Child Child { get; set; } = null!;
}