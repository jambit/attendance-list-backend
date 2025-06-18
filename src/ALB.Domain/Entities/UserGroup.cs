using ALB.Domain.Identity;

namespace ALB.Domain.Entities;

public class UserGroup
{
    public Guid UserId { get; set; }
    public Guid GroupId { get; set; }
    public bool IsSupervisor { get; set; }
    
    public ApplicationUser User { get; set; } = null!;
    public Group Group { get; set; } = null!;
}