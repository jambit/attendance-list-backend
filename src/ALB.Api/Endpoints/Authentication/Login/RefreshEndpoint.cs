using ALB.Application.UseCases.Auths;
using ALB.Domain.Repositories;

namespace ALB.Api.Endpoints.Authentication.Login;

internal static class RefreshEndpoint
{
    internal static IEndpointRouteBuilder MapRefreshEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/refresh", async (RefreshRequest refreshRequest, IRefreshTokenRepository repository, TokenProvider tokenProvider, CancellationToken ct) =>
        {
            var refreshToken = await repository.FindByRefreshTokenAsync(refreshRequest.RefreshToken, ct);

            if (refreshToken is null || refreshToken.ExpiresOnUtc < DateTime.UtcNow)
            {
                return Results.BadRequest("The refresh token has expired.");
            }

            return Results.Ok(
                new
                {
                    AccessToken = await tokenProvider.Create(refreshToken.User),
                    RefreshToken = await tokenProvider.UpdateTokenExpiration(refreshToken.Id, ct)
                });
        }).WithOpenApi()
        .RequireAuthorization();

        return endpoints;
    }
}

public record RefreshRequest(string RefreshToken);