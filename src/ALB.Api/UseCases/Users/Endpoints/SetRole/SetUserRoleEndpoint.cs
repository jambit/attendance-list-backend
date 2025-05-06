using ALB.Domain.Identity;
using ALB.Domain.Values;
using Microsoft.AspNetCore.Identity;

namespace ALB.Api.UseCases.Users.Endpoints.SetRole;

internal static class SetUserRoleEndpoint
{
    internal static void MapSetRoleEndpoint(this RouteGroupBuilder routeBuilder)
    {
        routeBuilder.MapPost("/roles", async (SetUserRoleRequest request, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager) =>
        {
            var userToAssignRoleTo = userManager.Users.FirstOrDefault(x => x.Id == request.UserId);

            if (userToAssignRoleTo is null)
                return Results.NotFound($"User with id: {request.UserId} does not exist.");
            
            var result = await userManager.AddToRoleAsync(userToAssignRoleTo, request.RoleId);
            return Results.Ok(new SetUserRoleResponse(result));
        })
            .RequireAuthorization(SystemRoles.AdminPolicy)
            .WithOpenApi();
    }
}

public record SetUserRoleRequest(Guid UserId, string RoleId);

public record SetUserRoleResponse(IdentityResult Result);