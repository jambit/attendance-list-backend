using ALB.Api.Extensions;
using ALB.Domain.Entities;
using ALB.Domain.Repositories;
using ALB.Domain.Values;
using FastEndpoints;
using NodaTime;

namespace ALB.Api.Endpoints.AttendanceList.AttendanceListEntries;

public class CreateAttendanceListEntryEndpoint(IAttendanceRepository repository)
    : Endpoint<CreateAttendanceListEntryRequest, CreateAttendanceListEntryResponse>
{
    public override void Configure()
    {
        Post("/api/attendance-lists/{attendanceListId:guid}/entries");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(CreateAttendanceListEntryRequest request, CancellationToken ct)
    {
        var attendanceListId = Route<Guid>("attendanceListId");
        var date = LocalDate.FromDateTime(request.Date);
        
        var exists = await repository.ExistsAsync(
            attendanceListId, 
            request.ChildId, 
            date, 
            ct);
        
        if (exists)
        {
            AddError("An attendance entry already exists for this child on this date.");
            await SendErrorsAsync(400, ct);
            return;
        }

        var entry = new AttendanceListEntry
        {
            Id = Guid.NewGuid(),
            AttendanceListId = attendanceListId,
            ChildId = request.ChildId,
            Date = date,
            ArrivalAt = request.ArrivalAt?.ToNodaLocalTime(),
            DepartureAt = request.DepartureAt?.ToNodaLocalTime(),
            AttendanceStatusId = request.AttendanceStatusId
        };

        var created = await repository.CreateAsync(entry, ct);

        await SendAsync(
            new CreateAttendanceListEntryResponse(
                created.Id,
                $"Attendance entry for child {request.ChildId} on {request.Date:yyyy-MM-dd} was successfully created."),
            200,
            ct);
    }
}

public record CreateAttendanceListEntryRequest(
    Guid ChildId,
    DateTime Date,
    DateTime? ArrivalAt,
    DateTime? DepartureAt,
    int AttendanceStatusId);

public record CreateAttendanceListEntryResponse(Guid Id, string Message);