using FastEndpoints;
using ALB.Api.Endpoints.TeamMember.DeleteAttendance;

namespace ALB.Api.Features.Endpoints.TeamMember.DeleteAttendance;

public class DeleteAttendanceEndpoint : Endpoint<DeleteAttendanceRequest, DeleteAttendanceResponse>
{
    public override void Configure()
    {
        Delete("/team-member/{id}/attendance");
        AllowAnonymous();
    }

    public async Task HandleAsync(DeleteAttendanceRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Attendance for {request.ChildName} at {request.Date} was deleted.");

        await SendAsync(new DeleteAttendanceResponse()
        {
            Message = $"Attendance for {request.ChildName} at {request.Date} was successfully deleted.",
        });
    }
}