namespace ALB.Api.UseCases.Endpoints.TeamMember.SetAttendance;

public record SetAttendanceRequest
{
    public Guid ChildId { get; set; }
    public string Status { get; set; }
    public DateTime Time { get; set; }

}