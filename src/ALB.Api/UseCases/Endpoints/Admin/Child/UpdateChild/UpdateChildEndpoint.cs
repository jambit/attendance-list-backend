using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Admin.Child.UpdateChild;

public class UpdateChildEndpoint : Endpoint<UpdateChildRequest, UpdateChildResponse>
{
    public override void Configure()
    {
        Put("/admin/child/update-children");
    }
    
    public override async Task HandleAsync(UpdateChildRequest request, CancellationToken cancellationToken)
    {
        await SendAsync(new UpdateChildResponse
        {
            Message = $"Update endpoint called for child ID: {request.Id}. (Not yet implemented)"
        }, cancellation: cancellationToken);
    }
}