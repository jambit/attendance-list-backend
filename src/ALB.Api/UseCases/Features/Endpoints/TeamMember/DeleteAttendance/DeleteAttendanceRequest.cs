namespace ALB.Api.Features.Endpoints.TeamMember.DeleteAttendance;

public class DeleteAttendanceRequest
{
    public string ChildName { get; set; }
    public DateTime Date { get; set; }
}