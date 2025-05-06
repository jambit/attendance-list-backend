using ALB.Domain.Identity;
using ALB.Domain.Values;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ALB.Api.UseCases.Users.Endpoints.Create;

internal static class CreateUserEndpoint
{
    internal static void MapCreateUserEndpoint(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("/", async Task<Results<Ok<CreateUserResponse>, BadRequest<ProblemDetails>>> (CreateUserRequest request, UserManager<ApplicationUser> userManager) =>
            {
                var userToCreate = new ApplicationUser
                {
                    Email = request.Email,
                    UserName = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    EmailConfirmed = true
                };
                
                var result = await userManager.CreateAsync(userToCreate, request.Password);

                if (!result.Succeeded)
                {
                    return TypedResults.Ok(new CreateUserResponse(userToCreate.Id, userToCreate.Email, userToCreate.FirstName, userToCreate.LastName));
                }

                return TypedResults.BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Could not create user.",
                    Detail = "User could not be created."
                });
            })
            .RequireAuthorization(SystemRoles.AdminPolicy)
            .WithOpenApi();
    }
}

public record CreateUserRequest(string Email, string Password, string? FirstName, string? LastName);

public record CreateUserResponse(Guid Id, string Email, string? FirstName, string? LastName);
