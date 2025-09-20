using ALB.Domain.Repositories;
using FastEndpoints;
using NodaTime;

namespace ALB.Api.Endpoints.AttendanceList.AttendanceListEntries;

public class DeleteAttendanceListEntryEndpoint(IAttendanceRepository repository)
    : Endpoint<DeleteAttendanceListEntryRequest, DeleteAttendanceListEntryResponse>
{
    public override void Configure()
    {
        Delete("/api/attendance-lists/{attendanceListId:guid}/entries");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteAttendanceListEntryRequest request, CancellationToken ct)
    {
        var attendanceListId = Route<Guid>("attendanceListId");
        var date = LocalDate.FromDateTime(request.Date);
        
        var attendanceListEntry = await repository.GetByListChildAndDateAsync(attendanceListId, request.ChildId, date, ct);

        if (attendanceListEntry is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        await repository.DeleteAsync(attendanceListEntry.Id, ct);

        await SendAsync(new DeleteAttendanceListEntryResponse(
            $"Attendance for {request.ChildId} at {request.Date} was successfully deleted."), cancellation: ct);
    }
}

public record DeleteAttendanceListEntryRequest(Guid ChildId, DateTime Date);

public record DeleteAttendanceListEntryResponse(string Message);