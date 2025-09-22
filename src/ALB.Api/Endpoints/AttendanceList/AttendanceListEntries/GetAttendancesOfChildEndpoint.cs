using ALB.Api.Extensions;
using ALB.Api.Models;
using ALB.Domain.Repositories;
using FastEndpoints;

namespace ALB.Api.Endpoints.AttendanceList.AttendanceListEntries;

public class GetAttendancesOfAttendanceListEndpoint(IAttendanceListRepository repository) : EndpointWithoutRequest<GetAttendancesOfAttendanceListResponse>
{
    public override void Configure()
    {
        Get("/api/attendance-lists/{attendanceListId:guid}/entries");
        Policies("AdminPolicy");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var attendanceListId = Route<Guid>("attendanceListId");
        
        var attendances = (await repository.GetAttendancesOfAttendanceListAsync(attendanceListId, ct))
            .Select(g => g.ToDto())
            .ToList();

        await SendAsync(new GetAttendancesOfAttendanceListResponse(attendances), cancellation: ct);
    }
}

public record GetAttendancesOfAttendanceListResponse(IEnumerable<AttendanceListEntryDto> AttendanceListEntries);