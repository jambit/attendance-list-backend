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
        var users = await userManager.Users.Select(u => u.ToDto()).ToListAsync(ct);

        await SendAsync(new GetUsersResponse(users), cancellation: ct);
    }
}

public record GetUsersResponse(IEnumerable<UserDto> Users);