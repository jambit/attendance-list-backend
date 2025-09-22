using ALB.Domain.Repositories;
using ALB.Domain.Values;
using FastEndpoints;

namespace ALB.Api.Endpoints.AttendanceList;

public class DeleteAttendanceListEndpoint(IAttendanceListRepository attendanceListRepository)
    : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("/api/attendance-lists/{attendanceListId:guid}");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var attendanceListId = Route<Guid>("attendanceListId");

        var attendanceList = await attendanceListRepository.GetByIdAsync(attendanceListId, ct);

        if (attendanceList is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await attendanceListRepository.DeleteAsync(attendanceListId, ct);

        await SendNoContentAsync(ct);
    }
}