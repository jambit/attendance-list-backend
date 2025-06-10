using System.ComponentModel.DataAnnotations;

namespace ALB.Domain.Entities;

public class Level
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public required string Description { get; set; }
    
    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
