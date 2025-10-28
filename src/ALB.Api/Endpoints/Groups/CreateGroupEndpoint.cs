using ALB.Domain.Repositories;
using ALB.Domain.Values;
using Group = ALB.Domain.Entities.Group;

namespace ALB.Api.Endpoints.Groups;

internal static class CreateGroupEndpoint
{
    internal static IEndpointRouteBuilder MapCreateGroupEndpoint(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("/", async (CreateGroupRequest request, IGroupRepository groupRepository, CancellationToken ct) =>
        {
            var group = new Group
            {
                Id = Guid.NewGuid(),
                Name = request.GroupName,
                ResponsibleUserId = request.ResponsibleUserId
            };

            var createdGroup = await groupRepository.CreateAsync(group);

            return Results.Created($"/groups/{createdGroup.Id}", new CreateGroupResponse(createdGroup.Id));
        }).WithName("CreateGroup")
            .WithGroupName("UsersManagement")
            .RequireAuthorization(SystemRoles.AdminPolicy);

        return routeBuilder;
    }
}

public record CreateGroupRequest(string GroupName, Guid ResponsibleUserId);

public record CreateGroupResponse(Guid Id);