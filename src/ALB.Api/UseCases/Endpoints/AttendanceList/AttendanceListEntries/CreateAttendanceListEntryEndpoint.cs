using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.AttendanceList.AttendanceListEntries;

public class CreateAttendanceListEntryEndpoint : Endpoint<CreateAttendanceListEntryRequest, CreateAttendanceListEntryResponse>
{
    public override void Configure()
    {
        Post("/api/attendance-lists/{attendanceListId:guid}/entries");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateAttendanceListEntryRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"[{request.Time}] {request.ChildId} is {request.Status}");

        await SendAsync(new CreateAttendanceListEntryResponse($"Attendance for {request.ChildId} at {request.Time} was successfully set to {request.Status}"));
    }
}

public record CreateAttendanceListEntryRequest(string ChildId, string Time, string Status);

public record CreateAttendanceListEntryResponse(string Message);