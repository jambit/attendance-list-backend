using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Admin.Group.UpdateGroup;

public class PutGroupEndpoint : Endpoint<PutGroupRequest, PutGroupResponse>
{
    public override void Configure()
    {
        Put("/api/groups/{groupId:Guid}");
    }

    public override async Task HandleAsync(PutGroupEndpoint request, CancellationToken cancellationToken)
    {
        await SendAsync(new PutGroupResponse("updated group with groupID: {groupId:Guid}"),
            cancellation: cancellationToken);
    }
}
