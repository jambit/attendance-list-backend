using ALB.Domain.Identity;

using Microsoft.AspNetCore.Identity;

namespace ALB.Api.Endpoints.Authentication.Login;

internal static class LoginEndpoint
{
    internal static IEndpointRouteBuilder MapLoginEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/login/jwt", async (LoginRequest request, UserManager<ApplicationUser> userManager, TokenProvider tokenProver, CancellationToken ct) =>
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user is null || !await userManager.CheckPasswordAsync(user, request.Password))
            {
                return Results.Unauthorized();
            }

            return Results.Ok(
                new
                {
                    AccessToken = await tokenProver.Create(user),
                    RefreshToken = await tokenProver.GenerateRefreshToken(user, ct)
                });

        }).WithOpenApi().AllowAnonymous();

        return endpoints;
    }

    public record LoginRequest(string Email, string Password);
}