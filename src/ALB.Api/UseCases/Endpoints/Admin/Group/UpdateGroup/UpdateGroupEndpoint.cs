using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Admin.Group.UpdateGroup;

public class UpdateGroupEndpoint : Endpoint<UpdateGroupRequest, UpdateGroupResponse>
{
    public override void Configure()
    {
        Put("/api/groups/{groupId:Guid}");
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
