using ALB.Api.Extensions;
using ALB.Domain.Enum;
using ALB.Domain.Repositories;
using FastEndpoints;
using NodaTime;

namespace ALB.Api.UseCases.Endpoints.AttendanceList.AttendanceListEntries;

public class UpdateAttendanceListEntryEndpoint(IAttendanceRepository repository)
    : Endpoint<UpdateAttendanceListEntryRequest, UpdateAttendanceListEntryResponse>
{
    public override void Configure()
    {
        Put("/api/attendance-lists/entries");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateAttendanceListEntryRequest request, CancellationToken ct)
    {
        await repository.UpdateAsync(request.ChildId, LocalDate.FromDateTime(request.Date), request.ArrivalAt.ToNodaLocalTime(), request.DepartureAt.ToNodaLocalTime(), request.Status, ct);

        await SendAsync(new UpdateAttendanceListEntryResponse(
            $"Attendance for {request.ChildId} at {LocalDate.FromDateTime(request.Date)} was successfully set to {request.Status}"));
    }
}


public record UpdateAttendanceListEntryRequest(Guid ChildId, DateTime Date, DateTime ArrivalAt, DateTime DepartureAt, AttendanceStatus Status);

public record UpdateAttendanceListEntryResponse(string Message);