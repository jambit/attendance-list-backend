using ALB.Api.Extensions;
using ALB.Api.Models;
using ALB.Domain.Identity;
using ALB.Domain.Values;
using Microsoft.AspNetCore.Identity;

namespace ALB.Api.Endpoints.Users;

internal static class GetUserEndpoint
{
    internal static IEndpointRouteBuilder MapGetUserEndpoint(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/{userId:guid}", async (Guid userId, UserManager<ApplicationUser> userManager, CancellationToken ct) =>
        {
            var user = await userManager.FindByIdAsync(userId.ToString());

            if (user is null)
            {
                return Results.NotFound();
            }

            var response = new GetUserResponse(user.ToDto());

            return Results.Ok(response);
        }).WithName("GetUser")
        .WithOpenApi()
        .RequireAuthorization(SystemRoles.AdminPolicy);
        
        return routeBuilder;
    }
}

public record GetUserResponse(UserDto User);