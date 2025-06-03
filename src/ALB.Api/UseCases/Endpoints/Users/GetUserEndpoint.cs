using ALB.Domain.Identity;
using ALB.Domain.Values;
using FastEndpoints;
using ALB.Infrastructure.Persistence.Repositories.Admin;

namespace ALB.Api.UseCases.Endpoints.Users;

public class GetUserEndpoint : EndpointWithoutRequest<GetUserResponse>
{
    private readonly IUserRepository _userRepository;

    public GetUserEndpoint(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public override void Configure()
    {
        Get("/api/users/{userId:guid}");
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

        var response = new GetUserResponse(
            user.Id,
            user.Email,
            user.FirstName,
            user.LastName
        );

        await SendAsync(response, cancellation: ct);
    }
}

public record GetUserResponse(Guid Id, string Email, string? FirstName, string? LastName);