using ALB.Api.Endpoints.AttendanceList;
using ALB.Api.Endpoints.Authentication;
using ALB.Api.Endpoints.Children;
using ALB.Api.Endpoints.Groups;
using ALB.Api.Endpoints.Users;

namespace ALB.Api.Endpoints;

internal static class EndpointsExtension
{
    internal static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        return routeBuilder
            .MapAuthEndpointsGroup()
            .MapAttendanceListEndpointsGroup()
            .MapChildrenEndpointsGroup()
            .MapUserEndpointsGroup()
            .MapGroupsEndpointsGroup();
    }
}