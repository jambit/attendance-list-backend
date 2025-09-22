using ALB.Api.Extensions;
using ALB.Api.Models;
using ALB.Domain.Repositories;
using ALB.Domain.Values;
using FastEndpoints;

namespace ALB.Api.Endpoints.Groups;

public class GetGroupEndpoint(IGroupRepository groupRepository) : EndpointWithoutRequest<GetGroupResponse>
{
    public override void Configure()
    {
        Get("/api/groups/{groupId:guid}");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var groupId = Route<Guid>("groupId");

        var group = await groupRepository.GetByIdAsync(groupId, ct);

        if (group is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var response = new GetGroupResponse(group.ToDto());

        await SendAsync(response, cancellation: ct);
    }
}

public record GetGroupResponse(GroupDto Group);