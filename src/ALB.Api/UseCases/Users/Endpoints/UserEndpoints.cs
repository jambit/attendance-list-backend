using ALB.Api.UseCases.Users.Endpoints.Create;
using ALB.Api.UseCases.Users.Endpoints.SetRole;

namespace ALB.Api.UseCases.Users.Endpoints;

internal static class UserEndpoints
{
    internal static void MapUserEndpoints(this IEndpointRouteBuilder groupBuilder)
    {
        var usersGroup = groupBuilder.MapGroup("users");

        usersGroup.MapSetRoleEndpoint();
        usersGroup.MapCreateUserEndpoint();
    }
}