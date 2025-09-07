using ALB.Domain.Repositories;
using FastEndpoints;
using NodaTime;

namespace ALB.Api.Endpoints.AttendanceList.AttendanceListEntries;

public class DeleteAttendanceListEntryEndpoint(IAttendanceRepository repository)
    : Endpoint<DeleteAttendanceListEntryRequest, DeleteAttendanceListEntryResponse>
{
    public override void Configure()
    {
        Delete("/api/attendance-lists/entries");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteAttendanceListEntryRequest request, CancellationToken ct)
    {
        await repository.DeleteAsync(request.ChildId, LocalDate.FromDateTime(request.Date), ct);

        await SendAsync(new DeleteAttendanceListEntryResponse(
            $"Attendance for {request.ChildId} at {request.Date} was successfully deleted."));
    }
}

public record DeleteAttendanceListEntryRequest(Guid ChildId, DateTime Date);

public record DeleteAttendanceListEntryResponse(string Message);