using ALB.Domain.Identity;

using NodaTime;

namespace ALB.Domain.Entities;

public class AttendanceListWriter
{
    public Guid UserId { get; set; }
    public Guid AttendanceListId { get; set; }
    public Instant AssignedAt { get; set; }

    public ApplicationUser User { get; set; } = null!;
    public AttendanceList AttendanceList { get; set; } = null!;
}