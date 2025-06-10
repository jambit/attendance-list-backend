using ALB.Infrastructure.Persistence.Repositories.Admin;
using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Groups.Children;

public class AddChildrenToGroupEndpoint(IGroupRepository repository)
    : Endpoint<AddChildToGroupRequest, AddChildToGroupResponse>
{
    public override void Configure()
    {
        Post("/api/groups/{groupId:guid}/children");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddChildToGroupRequest request, CancellationToken ct)
    {
        var groupId = Guid.Parse(request.GroupId);
        var childIds = request.ChildIds.Split(',').Select(Guid.Parse);

        await repository.AddChildrenToGroupAsync(groupId, childIds, ct);

        await SendAsync(new AddChildToGroupResponse(
            $"Children {request.ChildIds} were successfully added to group {request.GroupId}"));
    }
}


public record AddChildToGroupRequest(string ChildIds, string GroupId);

public record AddChildToGroupResponse(string Message);