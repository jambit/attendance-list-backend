using FastEndpoints; 

namespace ALB.Api.UseCases.Endpoints.Admin.ApplicationUser.CreateApplicationUser;

public class CreateUserEndpoint : Endpoint<CreateUserRequest, CreateUserResponse>
{
    public override void Configure()
    {
        //TODO route is a duplicate - added number 2 in this case
        Post("api/users2");
    }

    public override async Task HandleAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var newId = Guid.NewGuid();

        var response = new CreateUserResponse($"Created user {request.UserName}");

        await SendAsync(response, cancellation: cancellationToken);
    }
}