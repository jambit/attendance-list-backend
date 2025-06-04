using ALB.Domain.Identity;
using ALB.Domain.Values;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using ALB.Infrastructure.Persistence.Repositories.Admin;
namespace ALB.Api.UseCases.Endpoints.Users.Roles;

public class RemoveUserRoleEndpoint(IUserRoleRepository userRoleRepository, RoleManager<ApplicationRole> roleManager)
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

        try
        {
            var role = await roleManager.FindByNameAsync(request.Role);
            if (role is null)
            {
                AddError("Role not found.");
                await SendErrorsAsync(404, ct);
                return;
            }

            await userRoleRepository.RemoveRoleFromUserAsync(userId, role.Name);

            await SendAsync(new RemoveUserRoleResponse("Removed user role successfully."), cancellation: ct);
        }
        catch (Exception ex)
        {
            AddError(ex.Message);
            await SendErrorsAsync(500, ct);
        }
    }
}

public record RemoveUserRoleRequest(string Role);

public record RemoveUserRoleResponse(string Message);