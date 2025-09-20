namespace ALB.Api.Models;

public record AttendanceListDto(
    Guid Id,
    Guid CohortId,
    bool Open,
    DateTime ValidationStart,
    DateTime ValidationEnd
);