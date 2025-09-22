using ALB.Domain.Repositories;
using ALB.Domain.Values;
using FastEndpoints;

namespace ALB.Api.Endpoints.Groups;

public class UpdateGroupEndpoint(IGroupRepository groupRepository) : Endpoint<UpdateGroupRequest, UpdateGroupResponse>
{
    public override void Configure()
    {
        Put("/api/groups/{groupId:guid}");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(UpdateGroupRequest request, CancellationToken ct)
    {
        var groupId = Route<Guid>("groupId");

        var existingGroup = await groupRepository.GetByIdAsync(groupId, ct);

        if (existingGroup is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        existingGroup.Name = request.GroupName;

        await groupRepository.UpdateAsync(existingGroup, ct);

        await SendAsync(
            new UpdateGroupResponse($"Updated group '{existingGroup.Name}' with ID: {existingGroup.Id}"),
            cancellation: ct
        );
    }
}

public record UpdateGroupRequest(string GroupName);

public record UpdateGroupResponse(string Message);