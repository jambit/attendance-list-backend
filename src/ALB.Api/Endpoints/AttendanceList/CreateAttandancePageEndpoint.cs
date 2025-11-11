using ALB.Domain.Repositories;
using ALB.Domain.Values;

using NodaTime;

namespace ALB.Api.Endpoints.AttendanceList;

internal static class GetAttendancePageEndpoint
{
    internal static RouteGroupBuilder AddAttendancePageEndpoint(this RouteGroupBuilder builder)
    {
        builder.MapPost("/attendancelists/{attendanceListId:guid}/page", async (Guid attendanceListId, GetAttendancePageRequest request, IAttendanceRepository attendanceListRepo, IChildRepository childRepo, IAbsenceDayRepository absenceRepo) =>
        {
            var attendanceList = await attendanceListRepo.GetAttendanceListByIdAsync(attendanceListId);

            if (attendanceList is null)
            {
                return Results.NotFound();
            }

            var children = await childRepo.GetByCohortAsync(attendanceList.CohortId);

            var absences = await absenceRepo.GetByDateAsync(request.date);

            var dtos = children.Select(child =>
            {
                var absence = absences.FirstOrDefault(a => a.ChildId == child.Id);
                var status = absence?.AbsenceStatus.Name;
                return new AttendancePageChildDto(child.Id, child.FirstName, child.LastName, status);
            }).ToList();

            return Results.Ok(new GetAttendancePageResponse(dtos));
        }).WithName("GetAttendancePage")
            .WithOpenApi()
            .RequireAuthorization(SystemRoles.TeamPolicy);
        return builder;
    }
}

public record GetAttendancePageRequest(LocalDate date);

public record AttendancePageChildDto(Guid ChildId, string FirstName, string LastName, string Status);

public record GetAttendancePageResponse(List<AttendancePageChildDto> Children);