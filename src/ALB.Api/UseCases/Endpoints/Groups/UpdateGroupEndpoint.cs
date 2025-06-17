using ALB.Domain.Values;
using FastEndpoints;
using ALB.Domain.Repositories;

namespace ALB.Api.UseCases.Endpoints.Groups;

public class UpdateGroupEndpoint(IGroupRepository groupRepository) : Endpoint<UpdateGroupRequest, UpdateGroupResponse>
{
    public override void Configure()
    {
        Put("/api/groups/{groupId:guid}");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(UpdateGroupRequest request, CancellationToken cancellationToken)
    {
        var groupId = Route<Guid>("groupId");

        var existingGroup = await groupRepository.GetByIdAsync(groupId);

        if (existingGroup is null)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        existingGroup.Name = request.GroupName;

        await groupRepository.UpdateAsync(existingGroup);

        await SendAsync(
            new UpdateGroupResponse($"Updated group '{existingGroup.Name}' with ID: {existingGroup.Id}"),
            cancellation: cancellationToken
        );
    }
}

public record UpdateGroupRequest(Guid groupId, string GroupName);

public record UpdateGroupResponse(string Message);