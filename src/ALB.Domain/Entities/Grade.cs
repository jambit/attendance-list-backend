namespace ALB.Domain.Entities;

public class Grade
{
    public Guid Id { get; set; }
    public required string Description { get; set; }

    public Cohort Cohort { get; set; } = null!;
}
