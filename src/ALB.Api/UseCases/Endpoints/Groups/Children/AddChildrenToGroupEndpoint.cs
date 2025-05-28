using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Groups.Children;

public class AddChildrenToGroupEndpoint : Endpoint<AddChildToGroupRequest, AddChildToGroupResponse>
{
    public override void Configure()
    {
        Post("/api/groups/{groupId:guid}/children");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddChildToGroupRequest request, CancellationToken cancellationToken)
    {
        await SendAsync(new AddChildToGroupResponse($"Children {request.ChildIds} were successfully added to group {request.GroupId}"));
    }
}

public record AddChildToGroupRequest(string ChildIds, string GroupId);

public record AddChildToGroupResponse(string Message);