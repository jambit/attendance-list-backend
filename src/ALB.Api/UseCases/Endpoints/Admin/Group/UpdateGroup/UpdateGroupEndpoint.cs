using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Admin.Group.UpdateGroup;

public class UpdateGroupEndpoint : Endpoint<UpdateGroupRequest, UpdateGroupResponse>
{
    public override void Configure()
    {
        Put("/admin/group/update");
    }

    public override async Task HandleAsync(UpdateGroupRequest request, CancellationToken cancellationToken)
    {
        await SendAsync(new UpdateGroupResponse
        {
            Message = $"Update endpoint called for child ID: {request.Id}. (Not yet implemented)"
        }, cancellation: cancellationToken);
    }
}
