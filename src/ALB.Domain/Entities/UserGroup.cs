using ALB.Domain.Identity;

namespace ALB.Domain.Entities;

public class UserGroup
{
    public Guid Id { get; init; }
    
    public Guid UserId { get; init; }
    public ApplicationUser User { get; init; }
    
    public Guid GroupId { get; init; } // <==
    public Group Group { get; init; }
    
    public bool IsGroupAdmin { get; set; } = false;
}