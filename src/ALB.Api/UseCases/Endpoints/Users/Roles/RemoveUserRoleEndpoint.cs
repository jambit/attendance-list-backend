using ALB.Domain.Values;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;

namespace ALB.Api.UseCases.Endpoints.Users.Roles;

public class RemoveUserRoleEndpoint : EndpointWithoutRequest<RemoveUserRoleResponse>
{
    public override void Configure()
    {
        Delete("/api/users/{userId:guid}/roles");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        await SendAsync(new RemoveUserRoleResponse("Removed User Role"),
            cancellation: cancellationToken);
    }
}

public record RemoveUserRoleRequest(string Role);

public record RemoveUserRoleResponse(string Message);