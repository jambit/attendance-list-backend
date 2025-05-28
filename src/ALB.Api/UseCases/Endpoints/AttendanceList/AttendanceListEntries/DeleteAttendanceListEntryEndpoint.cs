using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.AttendanceList.AttendanceListEntries;

public class DeleteAttendanceListEntryEndpoint : Endpoint<DeleteAttendanceListEntryRequest, DeleteAttendanceListEntryResponse>
{
    public override void Configure()
    {
        Delete("/api/attendance-lists/{attendanceListId:guid}/entries/{entryId:guid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteAttendanceListEntryRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Attendance for {request.ChildId} at {request.Date} was deleted.");

        await SendAsync(new DeleteAttendanceListEntryResponse($"Attendance for {request.ChildId} at {request.Date} was successfully deleted."));
    }
}

public record DeleteAttendanceListEntryRequest(string ChildId, DateTime Date);

public record DeleteAttendanceListEntryResponse(string Message);