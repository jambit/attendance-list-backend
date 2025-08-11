using ALB.Domain.Identity;
using ALB.Domain.Values;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;

namespace ALB.Api.UseCases.Endpoints.Users.Roles;

public class AddUserRoleEndpoint(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    : Endpoint<AddUserRoleRequest>
{
    public override void Configure()
    {
        Post("/api/users/{userId:guid}/roles");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(AddUserRoleRequest request, CancellationToken ct)
    {
        var userId = Route<Guid>("userId");

        var userToAssignRoleTo = await userManager.FindByIdAsync(userId.ToString());

        if (userToAssignRoleTo is null)
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

        var result = await userManager.AddToRoleAsync(userToAssignRoleTo, request.Role);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors) AddError(error.Description);
            ThrowIfAnyErrors();
        }

        await SendNoContentAsync(ct);
    }
}

public record AddUserRoleRequest(string Role);