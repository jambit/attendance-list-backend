using ALB.Domain.Values;
using FastEndpoints;
using ALB.Infrastructure.Persistence.Repositories.Admin; 

namespace ALB.Api.UseCases.Endpoints.Users;

public class UpdateUserEndpoint : Endpoint<UpdateUserRequest, UpdateUserResponse>
{
    private readonly IUserRepository _userRepository; 
    
    public UpdateUserEndpoint(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public override void Configure()
    {
        Put("/api/users/{userId:guid}");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }
        
        user.Email = request.Email;
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;

        await _userRepository.UpdateAsync(user);

        await SendAsync(
            new UpdateUserResponse("User successfully updated"),
            cancellation: cancellationToken
        );
    }
}

public record UpdateUserRequest(Guid UserId, string Email, string? FirstName, string? LastName);

public record UpdateUserResponse(string Message);