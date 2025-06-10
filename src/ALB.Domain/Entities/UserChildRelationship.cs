using ALB.Domain.Enum;
using ALB.Domain.Identity;

namespace ALB.Domain.Entities;

public class UserChildRelationship 
{
    public Guid Id { get; init; }
    
    public Guid UserId { get; init; }
    public ApplicationUser User { get; init; }
    
    public Guid ChildId { get; init; }
    public Child Child { get; init; } 
}