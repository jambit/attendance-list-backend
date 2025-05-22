namespace ALB.Api.UseCases.Endpoints.GroupAdmin.RemoveChild;

public record RemoveChildRequest
{
    public Guid ChildId { get; set; }
   // public string GroupName { get; set; }
}