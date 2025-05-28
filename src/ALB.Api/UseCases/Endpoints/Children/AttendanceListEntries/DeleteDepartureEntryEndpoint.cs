using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Children.AttendanceListEntries;

public class DeleteDepartureEntryEndpoint : Endpoint<DeleteDepartureEntryRequest, DeleteDepartureEntryResponse>
{
    public override void Configure()
    {
        Delete("/api/children/{childId:guid}/departure-entries/{entryId:guid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteDepartureEntryRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Attendance for {request.ChildId} at {request.Date} was deleted.");

        await SendAsync(new DeleteDepartureEntryResponse($"Attendance for {request.ChildId} at {request.Date} was successfully deleted."));
    }
}

public record DeleteDepartureEntryRequest(string ChildId, DateTime Date);

public record DeleteDepartureEntryResponse(string Message);