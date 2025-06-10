using ALB.Domain.Enum;

namespace ALB.Domain.Entities;

public class AbsenceDay
{
    public Guid Id { get; set; }
    
    public Guid ChildId { get; set; }
    public virtual Child Child { get; set; }
    
    public AbsenceStatus AbsenceStatus { get; set; }
    
    public DateOnly Date { get; set; }
}