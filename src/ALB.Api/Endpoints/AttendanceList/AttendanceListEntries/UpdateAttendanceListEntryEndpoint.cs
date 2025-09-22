using ALB.Api.Extensions;
using ALB.Domain.Enum;
using ALB.Domain.Repositories;
using ALB.Domain.Values;
using FastEndpoints;
using NodaTime;

namespace ALB.Api.Endpoints.AttendanceList.AttendanceListEntries;

public class UpdateAttendanceListEntryEndpoint(IAttendanceRepository repository)
    : Endpoint<UpdateAttendanceListEntryRequest, UpdateAttendanceListEntryResponse>
{
    public override void Configure()
    {
        Put("/api/attendance-lists/entries");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(UpdateAttendanceListEntryRequest request, CancellationToken ct)
    {
        var attendanceListId = Route<Guid>("attendanceListId");
        var date = LocalDate.FromDateTime(request.Date);
        
        var attendanceListEntry = await repository.GetByListChildAndDateAsync(attendanceListId, request.ChildId, date, ct);

        if (attendanceListEntry is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        attendanceListEntry.ArrivalAt = request.ArrivalAt.ToNodaLocalTime();
        attendanceListEntry.DepartureAt = request.DepartureAt.ToNodaLocalTime();
        attendanceListEntry.AttendanceStatus = request.Status;
        
        await repository.UpdateAsync(attendanceListEntry, ct);

        await SendAsync(new UpdateAttendanceListEntryResponse(
            $"Attendance for {request.ChildId} at {request.Date} was successfully updated."), cancellation: ct);
    }
}

public record UpdateAttendanceListEntryRequest(
    Guid ChildId,
    DateTime Date,
    DateTime ArrivalAt,
    DateTime DepartureAt,
    AttendanceStatus Status);

public record UpdateAttendanceListEntryResponse(string Message);