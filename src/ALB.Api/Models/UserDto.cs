namespace ALB.Api.Models;

public record UserDto(
    Guid Id,
    string? Email,
    string? FirstName,
    string? LastName,
    bool EmailConfirmed,
    string? PhoneNumber
);