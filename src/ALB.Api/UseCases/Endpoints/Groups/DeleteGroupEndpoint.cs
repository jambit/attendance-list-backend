using ALB.Domain.Values;
using FastEndpoints;
using ALB.Infrastructure.Persistence.Repositories.Admin;

namespace ALB.Api.UseCases.Endpoints.Groups;

public class DeleteGroupEndpoint : Endpoint<DeleteGroupRequest, DeleteGroupResponse>
{
    private readonly IGroupRepository _groupRepository;

    public DeleteGroupEndpoint(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }
    public override void Configure()
    {
        Delete("/api/groups/{GroupId:guid}");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(DeleteGroupRequest request, CancellationToken cancellationToken)
    {
        await _groupRepository.DeleteAsync(request.GroupId);

        await SendAsync(new DeleteGroupResponse($"Deleted Group with Id: {request.GroupId}"),
            cancellation: cancellationToken);
    }
}

public record DeleteGroupRequest(Guid GroupId);
public record DeleteGroupResponse(string Message);