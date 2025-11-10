using ALB.Api.Endpoints.Authentication.Login;
using ALB.Api.Endpoints.Authentication.Logout;

namespace ALB.Api.Endpoints.Authentication;

internal static class AuthEndpointsGroup
{
    internal static void MapAuthEndpointsGroup(this IEndpointRouteBuilder routeBuilder)
    {
        var group = routeBuilder.MapGroup("/api/auth")
            .WithTags("Authentication");
            
        group.MapLogoutEndpoint()
            .MapLoginEndpoint()
            .MapRefreshEndpoint();
    }
}