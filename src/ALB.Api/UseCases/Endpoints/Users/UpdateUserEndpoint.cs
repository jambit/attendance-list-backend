using ALB.Domain.Values;
using FastEndpoints;
using ALB.Domain.Repositories;

namespace ALB.Api.UseCases.Endpoints.Users;

public class UpdateUserEndpoint(IUserRepository userRepository) : Endpoint<UpdateUserRequest, UpdateUserResponse>
{
    public override void Configure()
    {
        Put("/api/users/{userId:guid}");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }
        user.Email = request.Email;
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;

        await userRepository.UpdateAsync(user);
        
        await SendAsync(
            new UpdateUserResponse("User successfully updated"),
            cancellation: cancellationToken
        );
    }
}

public record UpdateUserRequest(Guid UserId, string? Email, string? FirstName, string? LastName);

public record UpdateUserResponse(string Message);