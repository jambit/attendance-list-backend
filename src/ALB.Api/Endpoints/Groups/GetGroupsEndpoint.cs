using ALB.Api.Extensions;
using ALB.Api.Models;
using ALB.Domain.Repositories;
using ALB.Domain.Values;
using FastEndpoints;

namespace ALB.Api.Endpoints.Groups;

public class GetGroupsEndpoint(IGroupRepository groupRepository) : EndpointWithoutRequest<GetGroupsResponse>
{
    public override void Configure()
    {
        Get("/api/groups/");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var groups = (await groupRepository.GetAllAsync(ct))
            .Select(g => g.ToDto())
            .ToList();

        await SendAsync(new GetGroupsResponse(groups), cancellation: ct);
    }
}

public record GetGroupsResponse(IEnumerable<GroupDto> Groups);