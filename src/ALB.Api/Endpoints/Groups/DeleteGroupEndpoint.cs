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

    public override async Task HandleAsync(DeleteGroupRequest request, CancellationToken ct)
    {
        await groupRepository.DeleteAsync(request.GroupId, ct);

        await SendAsync(new DeleteGroupResponse($"Deleted Group with Id: {request.GroupId}"),
            cancellation: ct);
    }
}

public record DeleteGroupRequest(Guid GroupId);

public record DeleteGroupResponse(string Message);