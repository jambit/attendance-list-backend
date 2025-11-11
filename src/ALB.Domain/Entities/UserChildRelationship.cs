using ALB.Domain.Identity;

namespace ALB.Domain.Entities;

public class UserChildRelationship
{
    public Guid UserId { get; set; }
    public Guid ChildId { get; set; }

    public ApplicationUser User { get; set; } = null!;
    public Child Child { get; set; } = null!;
}