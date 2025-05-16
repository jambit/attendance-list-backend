using FastEndpoints; 

namespace ALB.Api.UseCases.Endpoints.Admin.Group.DeleteGroup;

public class DeleteGroupEndpoint : Endpoint<DeleteGroupRequest, DeleteGroupResponse>
{
    public override void Configure()
    {
        Delete("/admin/groups/delete");
    }

    public override async Task HandleAsync(DeleteGroupRequest request, CancellationToken cancellationToken)
    {
        await SendAsync(new DeleteGroupResponse
        {
            Message = $"Delete endpoint called for group ID: {request.Id}. (Not yet implemented)"
        }, cancellation: cancellationToken);
    }
}