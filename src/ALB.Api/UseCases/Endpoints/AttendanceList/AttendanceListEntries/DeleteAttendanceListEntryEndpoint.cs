using ALB.Infrastructure.Persistence.Repositories.TeamMember;
using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.AttendanceList.AttendanceListEntries;

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
        var childId = Guid.Parse(request.ChildId);

        await repository.DeleteAsync(childId, request.Date, ct);

        await SendAsync(new DeleteAttendanceListEntryResponse(
            $"Attendance for {request.ChildId} at {request.Date} was successfully deleted."));
    }
}

public record DeleteAttendanceListEntryRequest(string ChildId, DateTime Date);

public record DeleteAttendanceListEntryResponse(string Message);