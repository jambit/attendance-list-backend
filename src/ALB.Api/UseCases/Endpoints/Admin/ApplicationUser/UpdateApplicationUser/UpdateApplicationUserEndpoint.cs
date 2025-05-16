using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Admin.ApplicationUser.UpdateApplicationUser;

public class UpdateApplicationUserEndpoint : Endpoint<UpdateApplicationUserRequest, UpdateApplicationUserResponse>
{
    public override void Configure()
    {
        Put("/admin/application-user/update-application-user");
    }

    public override async Task HandleAsync(UpdateApplicationUserRequest request, CancellationToken cancellationToken)
    {
        await SendAsync(new UpdateApplicationUserResponse
        {
            Message = "Application user is updated",
        }, cancellation: cancellationToken);
    }
}