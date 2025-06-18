using System.Text.Json.Serialization;
using ALB.Domain.Entities;

namespace ALB.Domain.Enum;

public class AbsenceStatus
{
    //Todo enumeration table
    public int Id { get; set; }
    [JsonIgnore]
    public ICollection<AbsenceDay> AbsenceDays { get; set; } = new List<AbsenceDay>();
}