using System.Text.Json.Serialization;
using ALB.Domain.Entities;

namespace ALB.Domain.Enum;

public class AttendanceStatus
{
    public int Id { get; set; }
    [JsonIgnore]
    public required string Name { get; set; }
    public ICollection<AttendanceListEntry> AttendanceListEntries { get; set; } = new List<AttendanceListEntry>();
}