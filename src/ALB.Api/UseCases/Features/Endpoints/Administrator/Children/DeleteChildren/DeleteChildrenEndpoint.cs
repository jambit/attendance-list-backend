using ALB.Api.UseCases.Features.Endpoints.Administrator.Children.CreateChildren;
using FastEndpoints; 


namespace ALB.Api.UseCases.Features.Endpoints.Administrator.Children.DeleteChildren;

public class DeleteChildrenEndpoint : Endpoint<DeleteChildrenRequest, DeleteChildrenResponse>
{
    public override void Configure()
    {
        Delete("/administrator/children/delete-children");
    }

    public override async Task HandleAsync(DeleteChildrenRequest request, CancellationToken cancellationToken)
    {
       var child = request.Id //Datenbankzugriff
           
       //Remove child von der datenbank -> wie genau keine ahnung
       
       await SendAsync(new DeleteChildrenResponse
       {
           Message = $"Child {child.ToString()} was deleted successfully."
       }, cancellation: cancellationToken);
    }
}