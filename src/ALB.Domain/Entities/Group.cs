using System.ComponentModel.DataAnnotations;
using ALB.Domain.Identity;

namespace ALB.Domain.Entities;

public class Group
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    
    public virtual ApplicationUser ResponsibleUser { get; set; }
    public virtual ICollection<Child> Children { get; set; } = new List<Child>();
    public virtual ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
    public virtual ICollection<ApplicationUser> Supervisors { get; set; } = new List<ApplicationUser>();
    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}