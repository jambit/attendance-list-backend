using ALB.Domain.Identity;
using ALB.Domain.Values;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;

namespace ALB.Api.Endpoints.Users;

public class UpdateUserEndpoint(UserManager<ApplicationUser> userManager)
    : Endpoint<UpdateUserRequest>
{
    public override void Configure()
    {
        Put("/api/users/{userId:guid}");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var userId = Route<Guid>("userId");

        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        if (!string.IsNullOrWhiteSpace(request.FirstName)) user.FirstName = request.FirstName;

        if (!string.IsNullOrWhiteSpace(request.LastName)) user.LastName = request.LastName;

        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors) AddError(error.Description);
            ThrowIfAnyErrors();
        }

        await SendNoContentAsync(cancellationToken);
    }
}

public record UpdateUserRequest(string? FirstName, string? LastName);