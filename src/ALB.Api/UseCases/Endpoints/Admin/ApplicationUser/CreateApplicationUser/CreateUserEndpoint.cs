using FastEndpoints; 

namespace ALB.Api.UseCases.Endpoints.Admin.ApplicationUser.CreateApplicationUser;

public class CreateUserEndpoint : Endpoint<CreateUserRequest, CreateUserResponse>
{
    public override void Configure()
    {
        Post("/api/users");
    }

    public override async Task HandleAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var newId = Guid.NewGuid();

        var response = new CreateUserResponse($"Created user {request.UserName}");

        await SendAsync(response, cancellation: cancellationToken);
    }
}