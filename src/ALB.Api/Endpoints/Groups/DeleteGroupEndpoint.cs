using ALB.Domain.Repositories;
using ALB.Domain.Values;
using FastEndpoints;

namespace ALB.Api.Endpoints.Groups;

public class DeleteGroupEndpoint(IGroupRepository groupRepository) : Endpoint<DeleteGroupRequest, DeleteGroupResponse>
{
    public override void Configure()
    {
        Delete("/api/groups/{GroupId:guid}");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(DeleteGroupRequest request, CancellationToken cancellationToken)
    {
        await groupRepository.DeleteAsync(request.GroupId);

        await SendAsync(new DeleteGroupResponse($"Deleted Group with Id: {request.GroupId}"),
            cancellation: cancellationToken);
    }
}

public record DeleteGroupRequest(Guid GroupId);

public record DeleteGroupResponse(string Message);