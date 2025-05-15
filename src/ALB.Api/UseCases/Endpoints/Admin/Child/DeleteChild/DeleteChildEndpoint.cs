using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Admin.Child.DeleteChild;

public class DeleteChildEndpoint : Endpoint<DeleteChildRequest, DeleteChildResponse>
{
    public override void Configure()
    {
        Delete("/administrator/child/delete-children");
    }

    public override async Task HandleAsync(DeleteChildRequest request, CancellationToken cancellationToken)
    {
        await SendAsync(new DeleteChildResponse
        {
            Message = $"Delete endpoint called for child ID: {request.Id}. (Not yet implemented)"
        }, cancellation: cancellationToken);
    }
}