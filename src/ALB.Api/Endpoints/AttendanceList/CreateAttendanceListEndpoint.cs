using ALB.Domain.Repositories;
using ALB.Domain.Values;
using FastEndpoints;
using NodaTime;

namespace ALB.Api.Endpoints.AttendanceList;

public class CreateAttendanceListEndpoint(IAttendanceListRepository attendanceListRepository)
    : Endpoint<CreateAttendanceListRequest, CreateAttendanceListResponse>
{
    public override void Configure()
    {
        Post("/api/attendance-lists");
        Policies(SystemRoles.AdminPolicy);
        Policies(SystemRoles.CoAdminPolicy);
    }

    public override async Task HandleAsync(CreateAttendanceListRequest request, CancellationToken ct)
    {
        var attendanceList = new Domain.Entities.AttendanceList
        {
            CohortId = request.CohortId,
            Open = request.Open,
            ValidationPeriod = new DateInterval(
                LocalDate.FromDateTime(request.ValidationStart),
                LocalDate.FromDateTime(request.ValidationEnd)
            )
        };

        var createdList = await attendanceListRepository.CreateAsync(attendanceList, ct);

        await SendAsync(
            new CreateAttendanceListResponse(createdList.Id, "Attendance list created successfully."),
            cancellation: ct
        );
    }
}

public record CreateAttendanceListRequest(
    Guid CohortId,
    bool Open,
    DateTime ValidationStart,
    DateTime ValidationEnd
);

public record CreateAttendanceListResponse(Guid Id, string Message);