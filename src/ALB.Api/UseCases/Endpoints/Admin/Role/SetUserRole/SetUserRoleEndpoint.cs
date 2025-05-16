using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Admin.Role.SetUserRole;

public class SetUserRoleEndpoint : Endpoint<SetUserRoleRequest, SetUserRoleResponse>
{
    public override void Configure()
    {
        Post("/admin/role/set-user-role");
    }

    public override async Task HandleAsync(SetUserRoleRequest request, CancellationToken cancellationToken)
    {
        await SendAsync(new SetUserRoleResponse
        {
            Message = "not implemented",
        }, cancellation: cancellationToken);
    }
}