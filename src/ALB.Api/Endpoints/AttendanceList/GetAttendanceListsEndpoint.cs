using ALB.Api.Extensions;
using ALB.Api.Models;
using ALB.Domain.Repositories;
using ALB.Domain.Values;
using FastEndpoints;

namespace ALB.Api.Endpoints.AttendanceList;

public class GetAttendanceListsEndpoint(IAttendanceListRepository attendanceListRepository)
    : EndpointWithoutRequest<GetAttendanceListsResponse>
{
    public override void Configure()
    {
        Get("/api/attendance-lists");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var attendanceLists = await attendanceListRepository.GetAllAsync(ct);

        var dtos = attendanceLists.Select(al => al.ToDto()).ToList();

        await SendAsync(new GetAttendanceListsResponse(dtos), cancellation: ct);
    }
}

public record GetAttendanceListsResponse(IEnumerable<AttendanceListDto> AttendanceLists);