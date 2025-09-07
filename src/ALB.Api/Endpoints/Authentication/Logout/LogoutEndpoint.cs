using ALB.Domain.Identity;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;

namespace ALB.Api.Endpoints.Authentication.Logout;

public class LogoutEndpoint(SignInManager<ApplicationUser> signInManager) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("/logout");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            await signInManager.SignOutAsync();

            await SendOkAsync(new LogoutResponse("Successfully logged out", DateTime.UtcNow), ct);
        }
        else
        {
            await SendOkAsync(new LogoutResponse("User was not logged in", DateTime.UtcNow), ct);
        }
    }
}

public record LogoutResponse(string Message, DateTime LoggedOutAt);