using ALB.Domain.Values;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using ALB.Infrastructure.Persistence.Repositories.Admin;

namespace ALB.Api.UseCases.Endpoints.Users.Roles;

public class AddUserRoleEndpoint : Endpoint<AddUserRoleRequest, AddUserRoleResponse>
{
    private readonly IUserRoleRepository _userRoleRepository;

    public AddUserRoleEndpoint(IUserRoleRepository userRoleRepository)
    {
        _userRoleRepository = userRoleRepository;
    }

    public override void Configure()
    {
        Post("/api/users/{userId:guid}/roles");
        Policies(SystemRoles.AdminPolicy);
    }
    
    public override async Task HandleAsync(AddUserRoleRequest request, CancellationToken ct)
    {
        var userId = Route<Guid>("userId");

        try
        {
            await _userRoleRepository.AssignRoleToUserAsync(userId, request.Role);

            await SendAsync(
                new AddUserRoleResponse(IdentityResult.Success),
                cancellation: ct);
        }
        catch (Exception ex)
        {
            AddError(ex.Message);
            await SendErrorsAsync(400, ct);
        }
    }
}

public record AddUserRoleRequest(string Role);

public record AddUserRoleResponse(IdentityResult Result);