namespace ALB.Api.UseCases.Endpoints.GroupAdmin.SetChild;

public class SetChildRequest
{
    public Guid ChildId { get; set; }
    public Guid GroupId { get; set; }
    public string GroupName { get; set; }
}