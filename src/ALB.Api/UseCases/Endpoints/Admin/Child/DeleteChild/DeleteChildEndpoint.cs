using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Admin.Child.DeleteChild;

public class DeleteChildEndpoint : EndpointWithoutRequest<DeleteChildResponse>
{
    public override void Configure()
    {
        Delete("/api/children/{id:Guid}");
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var id = Route<Guid>("id");
        
        
        await SendAsync(new DeleteChildResponse($"Deleted child with ID: {id}"),
        cancellation: cancellationToken);
    }
}