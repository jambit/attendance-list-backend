using System.Text.Json.Serialization;

using ALB.Domain.Entities;

namespace ALB.Domain.Enum;

public class AbsenceStatus
{
    public int Id { get; set; }
    public required string Name { get; set; }
    [JsonIgnore]
    public ICollection<AbsenceDay> AbsenceDays { get; set; } = new List<AbsenceDay>();
}