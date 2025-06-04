using ALB.Infrastructure.Persistence.Repositories.Admin;
using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Groups.Children;

public class RemoveChildrenFromGroupEndpoint : Endpoint<RemoveChildFromGroupRequest, RemoveChildFromGroupResponse>
{
    private readonly IGroupRepository _repository;

    public RemoveChildrenFromGroupEndpoint(IGroupRepository repository)
    {
        this._repository = repository;
    }

    public override void Configure()
    {
        Delete("/api/groups/{groupId:guid}/children");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RemoveChildFromGroupRequest request, CancellationToken ct)
    {
        var groupId = Route<Guid>("groupId");
        var childIds = request.ChildIds.Split(',').Select(Guid.Parse);

        await _repository.RemoveChildrenFromGroupAsync(groupId, childIds, ct);

        await SendAsync(new RemoveChildFromGroupResponse("Removed Children from Group"));
    }
}


public record RemoveChildFromGroupRequest(string ChildIds);

public record RemoveChildFromGroupResponse(string Message);