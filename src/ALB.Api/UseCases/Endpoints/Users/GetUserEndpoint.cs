using ALB.Api.Extensions;
using ALB.Api.Models;
using ALB.Domain.Identity;
using ALB.Domain.Values;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;

namespace ALB.Api.UseCases.Endpoints.Users;

public class GetUserEndpoint(UserManager<ApplicationUser> userManager) : EndpointWithoutRequest<GetUserResponse>
{
    public override void Configure()
    {
        Get("/api/users/{userId:guid}");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = Route<Guid>("userId");

        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var response = new GetUserResponse(user.ToDto());

        await SendAsync(response, cancellation: ct);
    }
}

public record GetUserResponse(UserDto User);