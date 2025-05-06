using FastEndpoints

namespace ALB.Api.UseCases.Features.Endpoints.Administrator.Children.UpdateChildren;

public class UpdateChildrenEndpoint : Endpoint<UpdateChildrenRequest, UpdateChildrenResponse>
{
    public override void Configure()
    {
        Put("/admin/children/update-children");
    }

    public override async Task HandleAsync(UpdateChildrenRequest request, CancellationToken cancellationToken)
    {
        var child = request.Id; //Datenbankzugriff

        child.Name = request.Name;
        child.DateofBirth = request.DateOfBirth;
        
        await SendAsync(new UpdateChildrenResponse
            {
                Message = $"Child {child.Name} was updated successfully."
            } cancellation: cancellationToken); 
    }
}