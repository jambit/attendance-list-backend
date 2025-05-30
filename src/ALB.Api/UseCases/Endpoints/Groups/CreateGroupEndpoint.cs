using ALB.Domain.Values;
using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Groups;

public class CreateGroupEndpoint : EndpointWithoutRequest<CreateGroupResponse>
{
    public override void Configure()
    {
        Post("/api/groups");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(CancellationToken cancellationToken) 
    {
        await SendAsync(
            new CreateGroupResponse(Guid.Empty, "Endpoint is not yet implemented."),
            cancellation: cancellationToken
        );
    }
}

public record CreateGroupRequest(string GroupName);

public record CreateGroupResponse(Guid Id, string Message);