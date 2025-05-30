using ALB.Domain.Values;
using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Children;

public class UpdateChildEndpoint : Endpoint<UpdateChildRequest, UpdateChildResponse>
{
    public override void Configure()
    {
        Put("/api/children/{childId:guid}");
        Policies(SystemRoles.AdminPolicy);
    }
    
    public override async Task HandleAsync(UpdateChildRequest request, CancellationToken cancellationToken)
    {
        await SendAsync(new UpdateChildResponse("updated child with ID: {Id:Guid}"),
            cancellation: cancellationToken);
    }
}

public record UpdateChildRequest(string ChildName);

public record UpdateChildResponse(string Message);