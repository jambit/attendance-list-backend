using ALB.Api.Extensions;
using ALB.Api.Models;
using ALB.Domain.Entities;
using ALB.Domain.Repositories;
using FastEndpoints;

namespace ALB.Api.Endpoints.Groups.Cohorts;

public class GetCohortsOfGroupEndpoint(IGroupRepository groupRepository) : EndpointWithoutRequest<GetCohortsOfGroupResponse>
{
    public override void Configure()
    {
        Get("/api/groups/{groupId:guid}/cohorts");
        Policies("AdminPolicy");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var groupId = Route<Guid>("groupId");
        
        var cohorts = (await groupRepository.GetAllCohortsOfGroupAsync(groupId, ct))
            .Select(g => g.ToDto())
            .ToList();

        await SendAsync(new GetCohortsOfGroupResponse(cohorts), cancellation: ct);
    }
}

public record GetCohortsOfGroupResponse(IEnumerable<CohortDto> Cohorts);