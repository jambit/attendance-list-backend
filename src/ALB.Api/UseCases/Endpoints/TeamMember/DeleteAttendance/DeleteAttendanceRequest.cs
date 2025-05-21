namespace ALB.Api.UseCases.Endpoints.TeamMember.DeleteAttendance;

public record DeleteAttendanceRequest
{
    public Guid ChildId { get; set; }
    public DateTime Date { get; set; }
}