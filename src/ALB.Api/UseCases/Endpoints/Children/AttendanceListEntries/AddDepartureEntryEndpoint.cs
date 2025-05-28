using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Children.AttendanceListEntries;

public class AddDepartureEntryEndpoint : Endpoint<AddDepartureEntryRequest, AddDepartureEntryResponse>
{
    public override void Configure()
    {
        Post("/api/children/{childId:guid}/departure-entries");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddDepartureEntryRequest request, CancellationToken cancellationToken)
    {
        await SendAsync(new AddDepartureEntryResponse($"Attendance for {request.ChildId} at {request.Time} was successfully set to {request.Status}"));
    }
}

public record AddDepartureEntryRequest(string ChildId, string Time, string Status);

public record AddDepartureEntryResponse(string Message);