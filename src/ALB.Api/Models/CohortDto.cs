namespace ALB.Api.Models;

public record CohortDto(
    Guid Id,
    int CreationYear,
    Guid GroupId,
    Guid GradeId);