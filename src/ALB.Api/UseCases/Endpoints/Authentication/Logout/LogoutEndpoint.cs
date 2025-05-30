using ALB.Domain.Identity;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;

namespace ALB.Api.UseCases.Endpoints.Authentication.Logout;

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
            
            await SendOkAsync(new LogoutResponse 
            { 
                Message = "Successfully logged out",
                LoggedOutAt = DateTime.UtcNow
            }, ct);
        }
        else
        {
            await SendOkAsync(new LogoutResponse 
            { 
                Message = "User was not logged in",
                LoggedOutAt = DateTime.UtcNow
            }, ct);
        }
    }
}