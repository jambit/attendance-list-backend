using ALB.Domain.Values;
using FastEndpoints;
using ALB.Infrastructure.Persistence.Adapters.Admin;

namespace ALB.Api.UseCases.Endpoints.Groups;

public class DeleteGroupEndpoint : Endpoint<DeleteGroupRequest, DeleteGroupResponse>
{
    private readonly IGroupAdapter _groupAdapter;

    public DeleteGroupEndpoint(IGroupAdapter groupAdapter)
    {
        _groupAdapter = groupAdapter;
    }
    public override void Configure()
    {
        Delete("/api/groups/{GroupId:guid}");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(DeleteGroupRequest request, CancellationToken cancellationToken)
    {
        await _groupAdapter.DeleteAsync(request.GroupId);

        await SendAsync(new DeleteGroupResponse($"Deleted Group with Id: {request.GroupId}"),
            cancellation: cancellationToken);
    }
}

public record DeleteGroupRequest(Guid GroupId);
public record DeleteGroupResponse(string Message);