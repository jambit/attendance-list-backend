namespace ALB.Api.UseCases.Endpoints.GroupAdmin.SetChild;

public record SetChildRequest
{
    public Guid ChildId { get; set; }
    public Guid GroupId { get; set; }
}