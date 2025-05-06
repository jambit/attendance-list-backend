namespace ALB.Api.UseCases.Users.Endpoints;

internal static class UserEndpoints
{
    internal static void MapUserEndpoints(this IEndpointRouteBuilder groupBuilder)
    {
        var usersGroup = groupBuilder.MapGroup("users");
    }
}