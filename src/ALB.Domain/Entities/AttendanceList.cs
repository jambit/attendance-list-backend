using ALB.Domain.Identity;

namespace ALB.Domain.Entities;

public class AttendanceList
{
    public Guid Id { get; set; }
    public bool Open { get; set; } = true;
    public Guid CohortId { get; set; }

    public Cohort Cohort { get; set; } = null!;
    public ICollection<AttendanceListEntry> AttendanceListEntries { get; set; } = new List<AttendanceListEntry>();
    public ICollection<AttendanceListWriter> Writers { get; set; } = new List<AttendanceListWriter>();

}