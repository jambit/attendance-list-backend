using FastEndpoints; 

namespace ALB.Api.UseCases.Endpoints.Admin.Group.DeleteGroup;

public class DeleteGroupEndpoint : EndpointWithoutRequest<DeleteGroupResponse>
{
    public override void Configure()
    {
        Delete("/api/groups/{groupId:Guid}");
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var GroupId = Route<Guid>("groupId");
        
        await SendAsync(new DeleteGroupResponse($"Deleted Endpoint for groop Id: {GroupId}"),
            cancellation: cancellationToken);
    }
}