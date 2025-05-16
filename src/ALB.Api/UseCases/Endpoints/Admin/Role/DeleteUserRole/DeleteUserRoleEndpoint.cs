using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Admin.Role.DeleteUserRole;

public class DeleteUserRoleEndpoint : Endpoint<DeleteUserRoleRequest, DeleteUserRoleResponse>
{
    public override void Configure()
    {
        Delete("/admin/role/delete-user-role");
    }

    public override async Task HandleAsync(DeleteUserRoleRequest request, CancellationToken cancellationToken)
    {
        await SendAsync(new DeleteUserRoleResponse
        {
            Message = $"UserRole deleted",
        }, cancellation: cancellationToken);
    }
}