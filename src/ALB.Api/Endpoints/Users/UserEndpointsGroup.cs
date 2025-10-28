using ALB.Api.Endpoints.Users.Roles;

namespace ALB.Api.Endpoints.Users;

internal static class UserEndpointsGroup
{
    internal static IEndpointRouteBuilder MapUserEndpointsGroup(this IEndpointRouteBuilder routeBuilder)
    {
        return routeBuilder.MapGroup("/api/users")
            .WithTags("Users Management")
            .WithGroupName("UsersManagement")
            .MapCreateUserEndpoint()
            .MapDeleteUserEndpoint()
            .MapGetUsersEndpoint()
            .MapGetUserEndpoint()
            .MapUpdateUserEndpoint()
            .MapAddUserRoleEndpoint()
            .MapRemoveUserRoleEndpoint();
    }
}