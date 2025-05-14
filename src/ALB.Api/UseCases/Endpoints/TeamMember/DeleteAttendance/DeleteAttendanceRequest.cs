namespace ALB.Api.UseCases.Endpoints.TeamMember.DeleteAttendance;

public class DeleteAttendanceRequest
{
    public string ChildName { get; set; }
    public DateTime Date { get; set; }
}