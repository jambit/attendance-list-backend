using ALB.Domain.Enum;
using NodaTime;

namespace ALB.Domain.Entities;

public class AttendanceListEntry
{
    public Guid Id { get; set; }
    public LocalDate Date { get; set; }
    public LocalTime? ArrivalAt { get; set; }
    public LocalTime? DepartureAt { get; set; }
    public Guid AttendanceListId { get; set; }
    public int AttendanceStatusId { get; set; }
    public Guid ChildId { get; set; }
    
    public AttendanceList AttendanceList { get; set; } = null!;
    public AttendanceStatus AttendanceStatus { get; set; } = null!;
    public Child Child { get; set; } = null!;
}