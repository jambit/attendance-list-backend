using NodaTime;

namespace ALB.Api.Models;

public record ChildDto(
    Guid Id,
    string FirstName,
    string LastName,
    LocalDate DateOfBirth,
    Guid? GroupId
);