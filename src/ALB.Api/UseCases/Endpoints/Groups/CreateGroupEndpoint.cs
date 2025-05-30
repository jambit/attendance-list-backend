using ALB.Domain.Values;
using FastEndpoints;
using ALB.Infrastructure.Persistence.Adapters.Admin;
using Group = ALB.Domain.Entities.Group;


namespace ALB.Api.UseCases.Endpoints.Groups;

public class CreateGroupEndpoint : Endpoint<CreateGroupRequest, CreateGroupResponse>
{
    private readonly IGroupAdapter _groupAdapter;

    public CreateGroupEndpoint(IGroupAdapter groupAdapter)
    {
        _groupAdapter = groupAdapter;
    }

    public override void Configure()
    {
        Post("/api/groups");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(CreateGroupRequest request, CancellationToken cancellationToken)
    {
        var group = new Group
        {
            Id = Guid.NewGuid(),
            Name = request.GroupName
        };

        var createdGroup = await _groupAdapter.CreateAsync(group);

        await SendAsync(
            new CreateGroupResponse(createdGroup.Id, "Group created successfully."),
            cancellation: cancellationToken
        );
    }
}

public record CreateGroupRequest(string GroupName);

public record CreateGroupResponse(Guid Id, string Message);