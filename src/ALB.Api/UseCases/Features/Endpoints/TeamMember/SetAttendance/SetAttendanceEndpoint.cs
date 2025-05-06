using FastEndpoints;


namespace ALB.Api.Features.Endpoints.TeamMember.SetAttendance;

public class SetAttendanceEndpoint : Endpoint<SetAttendanceRequest, SetAttendanceResponse>
{
    public override void Configure()
    {
        Post("/teammember/setattendance");
        AllowAnonymous();
    }

    public override async Task HandleAsync(SetAttendanceRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"[{request.Time}] {request.ChildName} is {request.Status}");

        await SendAsync(new SetAttendanceResponse
        {
            Message = $"Attendance for {request.ChildName} at {request.Time} was successfully set to {request.Status}"
        });
    }
}