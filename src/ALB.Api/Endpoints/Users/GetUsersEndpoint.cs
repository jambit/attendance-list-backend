using ALB.Api.Extensions;
using ALB.Api.Models;
using ALB.Domain.Identity;
using ALB.Domain.Values;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ALB.Api.Endpoints.Users;

public class GetUsersEndpoint(UserManager<ApplicationUser> userManager) : EndpointWithoutRequest<GetUsersResponse>
{
    public override void Configure()
    {
        Get("/api/users");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userDtos = await userManager.Users.Select(u => u.ToDto()).ToListAsync(ct);

        var response = new GetUsersResponse(userDtos);

        await SendAsync(response, cancellation: ct);
    }
}

public record GetUsersResponse(IEnumerable<UserDto> Users);