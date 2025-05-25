using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Admin.Role.SetUserRole;

public class SetUserRoleEndpoint : EndpointWithoutRequest<SetUserRoleResponse>
{
    public override void Configure()
    {
        Post("/api/users/{id:Guid}/roles/set");
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        await SendAsync(new SetUserRoleResponse("Set Role to User"),
            cancellation: cancellationToken);
    }
}