using ALB.Domain.Values;
using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Users;

public class UpdateUserEndpoint : Endpoint<UpdateUserRequest, UpdateUserResponse>
{
    public override void Configure()
    {
        Put("/api/users/{userId:guid}");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        await SendAsync(new UpdateUserResponse("Application User is updated"),
            cancellation: cancellationToken); 
    }
}

public record UpdateUserRequest(string Name, string Email);

public record UpdateUserResponse(string Message);