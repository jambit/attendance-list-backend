using System.Text.Json.Serialization;
using ALB.Domain.Entities;

namespace ALB.Domain.Enum;

public class AbsenceStatus
{
    public int Id { get; set; }
    [JsonIgnore]
    public required string Name { get; set; }
    public ICollection<AbsenceDay> AbsenceDays { get; set; } = new List<AbsenceDay>();
}