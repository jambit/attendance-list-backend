using ALB.Domain.Identity;
using ALB.Domain.Values;
using Microsoft.AspNetCore.Identity;

namespace ALB.Api.Endpoints.Users.Roles;

internal static class RemoveUserRoleEndpoint
{
    internal static IEndpointRouteBuilder MapRemoveUserRoleEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete("/{userId:guid}/roles",
            async (Guid userId, RemoveUserRoleRequest request, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, CancellationToken ct) =>
            {
                var user = await userManager.FindByIdAsync(userId.ToString());
                if (user is null)
                {
                    return Results.NotFound("User not found");
                }

                var roleExists = await roleManager.RoleExistsAsync(request.Role);
                if (!roleExists)
                {
                    return Results.NotFound("Role not found");
                }

                if (!await userManager.IsInRoleAsync(user, request.Role))
                    return Results.BadRequest($"User is not in role {request.Role}");

                var result = await userManager.RemoveFromRoleAsync(user, request.Role);
                if (!result.Succeeded)
                {
                    return Results.InternalServerError(result.Errors);
                }

                return Results.NoContent();
            }).WithName("RemoveUserRole")
            .WithOpenApi()
            .RequireAuthorization(SystemRoles.AdminPolicy);
        
        return endpoints;
    }
}

public record RemoveUserRoleRequest(string Role);