using ALB.Domain.Identity;
using ALB.Domain.Values;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ALB.Api.UseCases.Users.Endpoints.SetRole;

internal static class SetUserRoleEndpoint
{
    internal static void MapSetRoleEndpoint(this RouteGroupBuilder routeBuilder)
    {
        routeBuilder.MapPost("/roles", async Task<Results<Ok<SetUserRoleResponse>, NotFound<ProblemDetails>>> (SetUserRoleRequest request, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager) =>
        {
            var userToAssignRoleTo = userManager.Users.FirstOrDefault(x => x.Id == request.UserId);

            if (userToAssignRoleTo is null)
                return TypedResults.NotFound(new ProblemDetails
                    {
                        Status = StatusCodes.Status404NotFound,
                        Title = "Could not set role.",
                        Detail = $"User with id: {request.UserId} does not exist."
                    });
            
            var result = await userManager.AddToRoleAsync(userToAssignRoleTo, request.RoleValue);
            return TypedResults.Ok(new SetUserRoleResponse(result));
        })
            .RequireAuthorization(SystemRoles.AdminPolicy)
            .WithOpenApi();
    }
}

public record SetUserRoleRequest(Guid UserId, string RoleValue);

public record SetUserRoleResponse(IdentityResult Result);