using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Children.AttendanceListEntries;

public class DeleteArrivalEntryEndpoint : Endpoint<DeleteArrivalEntryRequest, DeleteArrivalEntryResponse>
{
    public override void Configure()
    {
        Delete("/api/children/{childId:guid}/arrival-entries/{entryId:guid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteArrivalEntryRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Attendance for {request.ChildId} at {request.Date} was deleted.");

        await SendAsync(new DeleteArrivalEntryResponse($"Attendance for {request.ChildId} at {request.Date} was successfully deleted."));
    }
}

public record DeleteArrivalEntryRequest(string ChildId, DateTime Date);

public record DeleteArrivalEntryResponse(string Message);