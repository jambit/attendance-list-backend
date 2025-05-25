using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Admin.Role.DeleteUserRole;

public class DeleteUserRoleEndpoint : EndpointWithoutRequest<DeleteUserRoleResponse>
{
    public override void Configure()
    {
        Delete("/api/users/{id:Guid}/roles");
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        await SendAsync(new DeleteUserRoleResponse("Deleted User"),
            cancellation: cancellationToken);
    }
}