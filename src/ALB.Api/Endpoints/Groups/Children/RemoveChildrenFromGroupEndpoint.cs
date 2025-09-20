using ALB.Domain.Repositories;
using ALB.Domain.Values;
using FastEndpoints;

namespace ALB.Api.Endpoints.Groups.Children;

public class RemoveChildrenFromGroupEndpoint(IGroupRepository repository)
    : Endpoint<RemoveChildFromGroupRequest, RemoveChildFromGroupResponse>
{
    public override void Configure()
    {
        Delete("/api/groups/{groupId:guid}/children");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(RemoveChildFromGroupRequest request, CancellationToken ct)
    {
        var groupId = Route<Guid>("groupId");
        var childIds = request.ChildIds.Split(',').Select(Guid.Parse);

        await repository.RemoveChildrenFromGroupAsync(groupId, childIds, ct);

        await SendAsync(new RemoveChildFromGroupResponse("Removed Children from Group"));
    }
}

public record RemoveChildFromGroupRequest(string ChildIds);

public record RemoveChildFromGroupResponse(string Message);