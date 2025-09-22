namespace ALB.Api.Models;

public record AttendanceListEntryDto(
    Guid Id,
    DateOnly Date,
    TimeOnly? ArrivalAt,
    TimeOnly? DepartureAt,
    Guid AttendanceListId,
    int AttendanceStatusId,
    Guid ChildId
    );