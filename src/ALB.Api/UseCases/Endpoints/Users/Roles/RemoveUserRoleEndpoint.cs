using ALB.Domain.Identity;
using ALB.Domain.Values;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;

namespace ALB.Api.UseCases.Endpoints.Users.Roles;

public class RemoveUserRoleEndpoint(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    : Endpoint<RemoveUserRoleRequest, RemoveUserRoleResponse>
{
    public override void Configure()
    {
        Delete("/api/users/{userId:guid}/roles");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(RemoveUserRoleRequest request, CancellationToken ct)
    {
        var userId = Route<Guid>("userId");

        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var roleExists = await roleManager.RoleExistsAsync(request.Role);
        if (!roleExists)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        if (!await userManager.IsInRoleAsync(user, request.Role))
            await SendAsync(new RemoveUserRoleResponse($"User is not in role {request.Role}"),
                StatusCodes.Status400BadRequest, ct);

        var result = await userManager.RemoveFromRoleAsync(user, request.Role);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors) AddError(error.Description);
            ThrowIfAnyErrors();
        }

        await SendNoContentAsync(ct);
    }
}

public record RemoveUserRoleRequest(string Role);

public record RemoveUserRoleResponse(string Message);