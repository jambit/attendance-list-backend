using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Admin.Group.CreateGroup;

public class CreateGroupEndpoint : EndpointWithoutRequest<CreateGroupResponse>
{
    public override void Configure()
    {
        Post("/api/groups");
    }

    public override async Task HandleAsync(CancellationToken cancellationToken) 
    {
        await SendAsync(
            new CreateGroupResponse(Guid.Empty, "Endpoint is not yet implemented."),
            cancellation: cancellationToken
        );
    }
}