using ALB.Api.Extensions;
using ALB.Api.Models;
using ALB.Domain.Identity;
using ALB.Domain.Values;
using ALB.Domain.Repositories;
using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Users;

public class GetUsersEndpoint(IUserRepository userRepository) : EndpointWithoutRequest<GetUsersResponse>
{
    public override void Configure()
    {
        Get("/api/users");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var users = await userRepository.GetAllAsync();

        var userDtos = users.Select(user => user.ToDto());

        var response = new GetUsersResponse(userDtos);

        await SendAsync(response, cancellation: ct);
    }
}

public record GetUsersResponse(IEnumerable<UserDto> Users);