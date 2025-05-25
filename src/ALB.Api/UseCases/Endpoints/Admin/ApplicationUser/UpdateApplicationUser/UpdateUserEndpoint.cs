using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Admin.ApplicationUser.UpdateApplicationUser;

public class UpdateUserEndpoint : Endpoint<UpdateUserRequest, UpdateUserResponse>
{
    public override void Configure()
    {
        Put("/api/users/{id:Guid}");
    }

    public override async Task HandleAsync(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        await SendAsync(new UpdateUserResponse("Application User is updated"),
            cancellation: cancellationToken); 
    }
}