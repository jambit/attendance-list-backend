using ALB.Domain.Enum;

namespace ALB.Domain.Entities;

public class Attendance
{
    public Guid Id { get; set; }
    
    public DateTime Date { get; set; }
    
    public DateTime? ArrivalAt { get; set; }
    
    public DateTime? DepartureAt { get; set; }
    
    public ChildStatus Status { get; set; } = ChildStatus.Absent;
    
    public Guid ChildId { get; set; }
    public virtual Child Child { get; set; }
}