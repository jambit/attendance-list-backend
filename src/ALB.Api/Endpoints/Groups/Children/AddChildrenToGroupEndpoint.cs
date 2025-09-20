using ALB.Domain.Repositories;
using ALB.Domain.Values;
using FastEndpoints;

namespace ALB.Api.Endpoints.Groups.Children;

public class AddChildrenToGroupEndpoint(IGroupRepository repository)
    : Endpoint<AddChildToGroupRequest, AddChildToGroupResponse>
{
    public override void Configure()
    {
        Post("/api/groups/{groupId:guid}/children");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(AddChildToGroupRequest request, CancellationToken ct)
    {
        var groupId = Route<Guid>("groupId");

        await repository.AddChildrenToGroupAsync(groupId, request.ChildIds, ct);

        await SendAsync(new AddChildToGroupResponse(
            $"Children with IDs {string.Join(", ", request.ChildIds)} were successfully added to group {groupId}"));
    }
}

public record AddChildToGroupRequest(List<Guid> ChildIds);

public record AddChildToGroupResponse(string Message);