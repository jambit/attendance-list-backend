using ALB.Api.Endpoints.Authentication.Logout;

namespace ALB.Api.Endpoints.Authentication;

internal static class AuthEndpointsGroup
{
    internal static IEndpointRouteBuilder MapAuthEndpointsGroup(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGroup("/api/auth")
            .WithTags("Authentication")
            .WithGroupName("Authentication")
            .MapLogoutEndpoint();
        
        return routeBuilder;
    }
}