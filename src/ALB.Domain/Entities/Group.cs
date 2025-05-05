using System.ComponentModel.DataAnnotations;

namespace ALB.Domain.Entities;

public class Group
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    
    public virtual ICollection<Child> Children { get; set; }
    public virtual ICollection<UserGroup> UserGroups { get; set; }
}