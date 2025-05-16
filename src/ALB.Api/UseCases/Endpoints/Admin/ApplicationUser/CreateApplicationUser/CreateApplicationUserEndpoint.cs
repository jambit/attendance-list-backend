using FastEndpoints; 

namespace ALB.Api.UseCases.Endpoints.Admin.ApplicationUser.CreateApplicationUser;

public class CreateApplicationUserEndpoint : Endpoint<CreateApplicationUserRequest, CreateApplicationUserResponse>
{
    public override void Configure()
    {
        Post("/admin/application-user/create-application-user");
    }

    public override async Task HandleAsync(CreateApplicationUserRequest request, CancellationToken cancellationToken)
    {
        await SendAsync(new CreateApplicationUserResponse
        {
            Id = Guid.Empty,
            Message = "not implemented",
        }, cancellation: cancellationToken);
    }
}