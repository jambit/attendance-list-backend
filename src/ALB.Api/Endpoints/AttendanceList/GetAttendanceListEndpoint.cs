using ALB.Domain.Repositories;
using ALB.Domain.Values;
using FastEndpoints;
using NodaTime;

namespace ALB.Api.Endpoints.AttendanceList;

public class GetAttendanceListEndpoint(IAttendanceListRepository attendanceListRepository)
    : EndpointWithoutRequest<GetAttendanceListResponse>
{
    public override void Configure()
    {
        Get("/api/attendance-lists/{attendanceListId:guid}");
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

        var response = new GetAttendanceListResponse(
            attendanceList.Id,
            attendanceList.CohortId,
            attendanceList.Open,
            attendanceList.ValidationPeriod.Start.ToDateTimeUnspecified(),
            attendanceList.ValidationPeriod.End.ToDateTimeUnspecified()
        );

        await SendAsync(response, cancellation: ct);
    }
}

public record GetAttendanceListResponse(
    Guid Id,
    Guid CohortId,
    bool Open,
    DateTime ValidationStart,
    DateTime ValidationEnd
);