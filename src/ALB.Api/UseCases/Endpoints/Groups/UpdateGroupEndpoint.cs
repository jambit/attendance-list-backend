using ALB.Domain.Values;
using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Groups;

public class UpdateGroupEndpoint : Endpoint<UpdateGroupRequest, UpdateGroupResponse>
{
    public override void Configure()
    {
        Put("/api/groups/{groupId:guid}");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(UpdateGroupRequest request, CancellationToken cancellationToken)
    {
        var GroupId = Route<Guid>("groupId");
        
        await SendAsync(
            new UpdateGroupResponse($"updated group with groupID: {GroupId}"),
            cancellation: cancellationToken
        );
    }
}

public record UpdateGroupRequest(string GroupName);

public record UpdateGroupResponse(string Message);