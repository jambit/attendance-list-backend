namespace ALB.Api.UseCases.Endpoints.GroupAdmin.UpdateChild;

public record UpdateChildRequest
{
    public Guid ChildId { get; set; }
    public Guid GroupId { get; set; }
}