using FastEndpoints;
using ALB.Domain.Repositories;
using ALB.Domain.Entities;
using ALB.Domain.Values;

namespace ALB.Api.UseCases.Endpoints.AttendanceLists;

public class GetAttendancePageEndpoint(
    IAttendanceRepository attendanceListRepo,
    IChildRepository childRepo,
    IAbsenceDayRepository absenceRepo
) : Endpoint<GetAttendancePageRequest, GetAttendancePageResponse>
{
    public override void Configure()
    {
        Get("/api/attendancelists/{AttendanceListId}/page");
        Policies(SystemRoles.TeamPolicy);
    }

    public override async Task HandleAsync(GetAttendancePageRequest request, CancellationToken ct)
    {
        var attendanceList = await attendanceListRepo.GetAttendanceListByIdAsync(request.AttendanceListId);
        
        if (attendanceList == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        var children = await childRepo.GetByCohortAsync(attendanceList.CohortId);
        
        var absences = await absenceRepo.GetByDateAsync(request.Date);
        
        var dtos = children.Select(child =>
        {
            var absence = absences.FirstOrDefault(a => a.ChildId == child.Id);
            var status = absence?.AbsenceStatus; // absenceStatus noch nicht definiert
            return new AttendancePageChildDto(child.Id, child.FirstName, child.LastName, status);
        }).ToList();

        await SendAsync(new GetAttendancePageResponse(dtos), cancellation: ct);
    }
}

public record GetAttendancePageRequest(Guid AttendanceListId, DateOnly Date);
public record AttendancePageChildDto(Guid ChildId, string FirstName, string LastName, string Status);
public record GetAttendancePageResponse(List<AttendancePageChildDto> Children);