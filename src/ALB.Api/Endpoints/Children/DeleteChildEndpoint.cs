using ALB.Domain.Repositories;
using ALB.Domain.Values;

namespace ALB.Api.Endpoints.Children;

internal static class DeleteChildEndpoint
{
    internal static RouteGroupBuilder AddDeleteChildEndpoint(this RouteGroupBuilder builder)
    {
        builder.MapDelete("/{childId:guid}", async (Guid childId, IChildRepository childRepository) =>
        {
            var child = await childRepository.GetByIdAsync(childId);

            if (child is null)
            {
                return Results.NotFound();
            }

            await childRepository.DeleteAsync(childId);

            return Results.NoContent();
        }).WithName("DeleteChild").WithOpenApi()
            .RequireAuthorization(policy => policy.RequireRole(SystemRoles.Admin));

        return builder;
    }
}