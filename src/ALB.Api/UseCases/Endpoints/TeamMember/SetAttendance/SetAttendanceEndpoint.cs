using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.TeamMember.SetAttendance;

public class SetAttendanceEndpoint : Endpoint<SetAttendanceRequest, SetAttendanceResponse>
{
    public override void Configure()
    {
        Post("api/attendance");
        AllowAnonymous();
    }

    public override async Task HandleAsync(SetAttendanceRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"[{request.Time}] {request.ChildId} is {request.Status}");

        await SendAsync(new SetAttendanceResponse
        {
            Message = $"Attendance for {request.ChildId} at {request.Time} was successfully set to {request.Status}"
        });
    }
}