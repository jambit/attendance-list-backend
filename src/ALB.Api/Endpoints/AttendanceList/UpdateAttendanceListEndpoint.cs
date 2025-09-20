using ALB.Domain.Repositories;
using ALB.Domain.Values;
using FastEndpoints;
using NodaTime;

namespace ALB.Api.Endpoints.AttendanceList;

public class UpdateAttendanceListEndpoint(IAttendanceListRepository attendanceListRepository)
    : Endpoint<UpdateAttendanceListRequest>
{
    public override void Configure()
    {
        Put("/api/attendance-lists/{attendanceListId:guid}");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(UpdateAttendanceListRequest request, CancellationToken ct)
    {
        var attendanceListId = Route<Guid>("attendanceListId");

        var existingList = await attendanceListRepository.GetByIdAsync(attendanceListId, ct);

        if (existingList is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        existingList.Open = request.Open;
        existingList.ValidationPeriod = new DateInterval(
            LocalDate.FromDateTime(request.ValidationStart),
            LocalDate.FromDateTime(request.ValidationEnd)
        );

        await attendanceListRepository.UpdateAsync(existingList, ct);

        await SendNoContentAsync(ct);
    }
}

public record UpdateAttendanceListRequest(
    bool Open,
    DateTime ValidationStart,
    DateTime ValidationEnd
);