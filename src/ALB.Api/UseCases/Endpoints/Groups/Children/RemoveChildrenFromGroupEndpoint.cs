using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Groups.Children;

public class RemoveChildrenFromGroupEndpoint : Endpoint<RemoveChildFromGroupRequest, RemoveChildFromGroupResponse>
{
    public override void Configure()
    {
        Delete("/api/groups/{groupId:guid}/children");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RemoveChildFromGroupRequest fromGroupRequest, CancellationToken cancellationToken)
    {
        await SendAsync(new RemoveChildFromGroupResponse("Removed Children from Group"));
    }
}

public record RemoveChildFromGroupRequest(string ChildIds);

public record RemoveChildFromGroupResponse(string Message);