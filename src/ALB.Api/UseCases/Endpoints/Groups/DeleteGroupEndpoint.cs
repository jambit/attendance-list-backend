using ALB.Domain.Values;
using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Groups;

public class DeleteGroupEndpoint : EndpointWithoutRequest<DeleteGroupResponse>
{
    public override void Configure()
    {
        Delete("/api/groups/{groupId:guid}");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var GroupId = Route<Guid>("groupId");
        
        await SendAsync(new DeleteGroupResponse($"Deleted Endpoint for groop Id: {GroupId}"),
            cancellation: cancellationToken);
    }
}

public record DeleteGroupRequest(string GroupName);
public record DeleteGroupResponse(string Message);