using ALB.Domain.Enum;

using NodaTime;

namespace ALB.Domain.Entities;

public class AbsenceDay
{
    public Guid Id { get; set; }
    public LocalDate Date { get; set; }
    public Guid ChildId { get; set; }
    public int AbsenceStatusId { get; set; }


    public Child Child { get; set; } = null!;
    public AbsenceStatus AbsenceStatus { get; set; } = null!;
}