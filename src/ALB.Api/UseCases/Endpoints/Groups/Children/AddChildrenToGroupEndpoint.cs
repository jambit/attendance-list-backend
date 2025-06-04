using ALB.Infrastructure.Persistence.Repositories.Admin;
using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Groups.Children;

public class AddChildrenToGroupEndpoint : Endpoint<AddChildToGroupRequest, AddChildToGroupResponse>
{
    private readonly IGroupRepository _repository;

    public AddChildrenToGroupEndpoint(IGroupRepository repository)
    {
        this._repository = repository;
    }

    public override void Configure()
    {
        Post("/api/groups/{groupId:guid}/children");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddChildToGroupRequest request, CancellationToken ct)
    {
        var groupId = Guid.Parse(request.GroupId);
        var childIds = request.ChildIds.Split(',').Select(Guid.Parse);

        await _repository.AddChildrenToGroupAsync(groupId, childIds, ct);

        await SendAsync(new AddChildToGroupResponse(
            $"Children {request.ChildIds} were successfully added to group {request.GroupId}"));
    }
}


public record AddChildToGroupRequest(string ChildIds, string GroupId);

public record AddChildToGroupResponse(string Message);