using ALB.Api.Extensions;
using ALB.Domain.Enum;
using ALB.Domain.Repositories;
using FastEndpoints;
using NodaTime;

namespace ALB.Api.Endpoints.AttendanceList.AttendanceListEntries;

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
        await repository.CreateAsync(request.ChildId, LocalDate.FromDateTime(request.Date),
            request.ArrivalAt.ToNodaLocalTime(), request.DepartureAt.ToNodaLocalTime(), request.Status, ct);

        await SendAsync(new CreateAttendanceListEntryResponse(
            $"Attendance for {request.ChildId} at {LocalDate.FromDateTime(request.Date)} was successfully set to {request.Status}"));
    }
}

public record CreateAttendanceListEntryRequest(
    Guid ChildId,
    DateTime Date,
    DateTime ArrivalAt,
    DateTime DepartureAt,
    AttendanceStatus Status);

public record CreateAttendanceListEntryResponse(string Message);