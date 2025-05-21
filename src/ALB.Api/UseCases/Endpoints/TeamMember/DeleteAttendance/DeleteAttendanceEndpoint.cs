using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.TeamMember.DeleteAttendance;

public class DeleteAttendanceEndpoint : Endpoint<DeleteAttendanceRequest, DeleteAttendanceResponse>
{
    public override void Configure()
    {
        Delete("api/attendance/{attendanceid}");
        AllowAnonymous();
    }

    public async Task HandleAsync(DeleteAttendanceRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Attendance for {request.ChildId} at {request.Date} was deleted.");

        await SendAsync(new DeleteAttendanceResponse()
        {
            Message = $"Attendance for {request.ChildId} at {request.Date} was successfully deleted.",
        });
    }
}