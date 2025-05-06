namespace ALB.Api.Features.Endpoints.TeamMember.SetAttendance;

public class SetAttendanceRequest
{
    public string ChildName { get; set; }
    public string Status { get; set; }
    public DateTime Time { get; set; }
    
}