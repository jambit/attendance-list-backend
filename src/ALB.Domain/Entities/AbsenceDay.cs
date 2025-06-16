using ALB.Domain.Enum;

namespace ALB.Domain.Entities;

public class AbsenceDay
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public Guid ChildId { get; set; }
    public int AbsenceStatusId { get; set; }

    
    public Child Child { get; set; } = null!;
    public AbsenceStatus AbsenceStatus { get; set; } = null!;
}