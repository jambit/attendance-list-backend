using ALB.Domain.Enum;
using ALB.Infrastructure.Persistence.Repositories.TeamMember;
using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.AttendanceList.AttendanceListEntries;

public class CreateAttendanceListEntryEndpoint(IAttendanceRepository repository)
    : Endpoint<CreateAttendanceListEntryRequest, CreateAttendanceListEntryResponse>
{
    public override void Configure()
    {
        Post("/api/attendance-lists/entries");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateAttendanceListEntryRequest request, CancellationToken ct)
    {
        var childId = Guid.Parse(request.ChildId);
        var time = DateTime.Parse(request.Time);
        var status = Enum.Parse<ChildStatus>(request.Status);

        await repository.CreateOrUpdateAsync(childId, time.Date, time, null, status, ct);

        await SendAsync(new CreateAttendanceListEntryResponse(
            $"Attendance for {request.ChildId} at {request.Time} was successfully set to {request.Status}"));
    }
}



public record CreateAttendanceListEntryRequest(string ChildId, string Time, string Status);

public record CreateAttendanceListEntryResponse(string Message);