using System.Text.Json.Serialization;
using ALB.Domain.Entities;

namespace ALB.Domain.Enum;

public class AttendanceStatus
{
    //Todo enumeration table
    public int Id { get; set; }
    [JsonIgnore]
    public ICollection<AttendanceListEntry> AttendanceListEntries { get; set; } = new List<AttendanceListEntry>();
}