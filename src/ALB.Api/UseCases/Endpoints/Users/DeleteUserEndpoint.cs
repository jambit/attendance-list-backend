using ALB.Domain.Identity;
using ALB.Domain.Values;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;

namespace ALB.Api.UseCases.Endpoints.Users;

public class DeleteUserEndpoint(UserManager<ApplicationUser> userManager) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("/api/users/{userId:guid}");
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

        var result = await userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            await SendAsync(new { error = errors }, StatusCodes.Status400BadRequest, ct);
            return;
        }

        await SendNoContentAsync(ct);
    }
}