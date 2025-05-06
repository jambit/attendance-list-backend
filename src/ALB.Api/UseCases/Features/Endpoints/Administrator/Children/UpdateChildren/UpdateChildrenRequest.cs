namespace ALB.Api.UseCases.Features.Endpoints.Administrator.Children.UpdateChildren;

public class UpdateChildrenRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
}