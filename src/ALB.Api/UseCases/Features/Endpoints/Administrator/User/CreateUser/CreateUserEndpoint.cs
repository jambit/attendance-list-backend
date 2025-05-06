using FastEndpoints

namespace ALB.Api.UseCases.Features.Endpoints.Administrator.User.CreateUser;

public class CreateUserEndpoint : Endpoint<CreateUserRequest, CreateUserResponse>
{
    public override void Configure()
    {
        Post("/admin/children/create-user");
    }

    public override async Task HandleAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var createdUserId = Guid.NewGuid(); //Datenbankzugriff -> erstellen eines neuen Users

        await SendAsync(new CreateUserResponse
        {
            Id = createdUserId,
            Message = $"Child {createdUserId} created successfully",
        });
    }
}
