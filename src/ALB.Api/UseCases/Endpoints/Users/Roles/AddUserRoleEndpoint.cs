using ALB.Domain.Identity;
using ALB.Domain.Values;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace ALB.Api.UseCases.Endpoints.Users.Roles;

public class AddUserRoleEndpoint(UserManager<ApplicationUser> userManager)
    : Endpoint<AddUserRoleRequest, AddUserRoleResponse>
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
            await SendAsync(
                new AddUserRoleResponse(Result: IdentityResult.Failed(new IdentityError {Description = "User not found."})),
                404,
                cancellation: ct);
            return;
        }
        
        await SendAsync(
            new AddUserRoleResponse(Result: await userManager.AddToRoleAsync(userToAssignRoleTo, request.Role)),
            200, ct);
    }
}

public record AddUserRoleRequest(string Role);

public record AddUserRoleResponse(IdentityResult Result);