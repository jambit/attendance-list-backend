using ALB.Infrastructure.Persistence.Adapters.Admin;
using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Groups.Children;

public class RemoveChildrenFromGroupEndpoint : Endpoint<RemoveChildFromGroupRequest, RemoveChildFromGroupResponse>
{
    private readonly IGroupAdapter adapter;

    public RemoveChildrenFromGroupEndpoint(IGroupAdapter adapter)
    {
        this.adapter = adapter;
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

        await adapter.RemoveChildrenFromGroupAsync(groupId, childIds, ct);

        await SendAsync(new RemoveChildFromGroupResponse("Removed Children from Group"));
    }
}


public record RemoveChildFromGroupRequest(string ChildIds);

public record RemoveChildFromGroupResponse(string Message);