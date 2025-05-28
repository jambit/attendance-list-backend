using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Children.AttendanceListEntries;

public class AddArrivalEntryEndpoint : Endpoint<AddArrivalEntryRequest, AddArrivalEntryResponse>
{
    public override void Configure()
    {
        Post("/api/children/{childId:guid}/arrival-entries");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddArrivalEntryRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"[{request.Time}] {request.ChildId} is {request.Status}");

        await SendAsync(new AddArrivalEntryResponse($"Attendance for {request.ChildId} at {request.Time} was successfully set to {request.Status}"));
    }
}

public record AddArrivalEntryRequest(string ChildId, string Time, string Status);

public record AddArrivalEntryResponse(string Message);