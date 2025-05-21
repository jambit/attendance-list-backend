using ALB.Domain.Identity;
using ALB.Domain.Values;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace ALB.Api.UseCases.Users.Endpoints.SetRole;

public class SetUserRoleEndpoint(UserManager<ApplicationUser> userManager)
    : Endpoint<SetUserRoleRequest, SetUserRoleResponse>
{
    public override void Configure()
    {
        Post("api/users/roles");
        Policies(SystemRoles.AdminPolicy);
    }
    
    public override async Task HandleAsync(SetUserRoleRequest request, CancellationToken ct)
    {
        
        var userToAssignRoleTo = userManager.Users.FirstOrDefault(x => x.Id == request.UserId);

        if (userToAssignRoleTo is null)
        {
            await SendAsync(
                new SetUserRoleResponse
                {
                    Result = IdentityResult.Failed(new IdentityError {Description = "User not found."})
                },
                404,
                cancellation: ct);
            ThrowIfAnyErrors();
        }
        
        await SendAsync(new SetUserRoleResponse
        {
            Result = await userManager.AddToRoleAsync(userToAssignRoleTo, request.RoleValue)
        },200, ct);
    }
}

public class SetUserRoleRequest
{
    public Guid UserId { get; set; }
    public string RoleValue { get; set; }
}

public class SetUserRoleResponse
{
    public IdentityResult Result { get; set; }
}