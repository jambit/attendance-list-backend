using ALB.Domain.Repositories;
using ALB.Domain.Values;

namespace ALB.Api.Endpoints.Groups.Children;

internal static class RemoveChildrenFromGroupEndpoint
{
    internal static IEndpointRouteBuilder MapRemoveChildrenFromGroupEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete("/{groupId:guid}/children", async (Guid groupId, RemoveChildFromGroupRequest request, IGroupRepository repository, CancellationToken ct) =>
        {
            await repository.RemoveChildrenFromGroupAsync(groupId, request.ChildIds, ct);

            return Results.NoContent();
        }).WithName("RemoveChildrenFromGroup")
        .WithOpenApi()
        .RequireAuthorization(SystemRoles.AdminPolicy);

        return endpoints;
    }
}

public record RemoveChildFromGroupRequest(List<Guid> ChildIds);