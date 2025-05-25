using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Admin.Child.UpdateChild;

public class UpdateChildEndpoint : Endpoint<UpdateChildRequest, UpdateChildResponse>
{
    public override void Configure()
    {
        Put("/api/children/{Id:Guid}");
    }
    
    public override async Task HandleAsync(UpdateChildRequest request, CancellationToken cancellationToken)
    {
        await SendAsync(new UpdateChildResponse("updated child with ID: {Id:Guid}"),
            cancellation: cancellationToken);
    }
}