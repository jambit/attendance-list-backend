namespace ALB.Api.Models;

public record GroupDto(
    Guid Id,
    string? Name,
    Guid ResponsibleUserId
);