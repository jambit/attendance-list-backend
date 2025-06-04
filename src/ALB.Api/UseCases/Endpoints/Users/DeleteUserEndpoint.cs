using ALB.Domain.Values;
using FastEndpoints;
using ALB.Infrastructure.Persistence.Repositories.Admin;

namespace ALB.Api.UseCases.Endpoints.Users;

public class DeleteUserEndpoint : EndpointWithoutRequest<DeleteUserResponse>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserEndpoint(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public override void Configure()
    {
        Delete("/api/users/{userId:guid}");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = Route<Guid>("userId");

        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await _userRepository.DeleteAsync(userId);

        await SendAsync(new DeleteUserResponse("User successfully deleted"), cancellation: ct);
    }
}

public record DeleteUserResponse(string Message);