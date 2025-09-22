using ALB.Domain.Repositories;
using ALB.Domain.Values;
using FastEndpoints;
using Group = ALB.Domain.Entities.Group;

namespace ALB.Api.Endpoints.Groups;

public class CreateGroupEndpoint(IGroupRepository groupRepository) : Endpoint<CreateGroupRequest, CreateGroupResponse>
{
    public override void Configure()
    {
        Post("/api/groups");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(CreateGroupRequest request, CancellationToken ct)
    {
        var group = new Group
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            ResponsibleUserId = request.ResponsibleUserId
        };

        var createdGroup = await groupRepository.CreateAsync(group, ct);

        await SendAsync(
            new CreateGroupResponse(createdGroup.Id, "Group created successfully."),
            cancellation: ct
        );
    }
}

public record CreateGroupRequest(string Name, Guid ResponsibleUserId);

public record CreateGroupResponse(Guid Id, string Message);