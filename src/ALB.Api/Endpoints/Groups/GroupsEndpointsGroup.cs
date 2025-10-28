using ALB.Api.Endpoints.Groups.Children;
using ALB.Api.Endpoints.Groups.Cohorts;

namespace ALB.Api.Endpoints.Groups;

internal static class GroupsEndpointsGroup
{
    internal static IEndpointRouteBuilder MapGroupsEndpointsGroup(this IEndpointRouteBuilder routeBuilder)
    {
        return routeBuilder.MapGroup("/api/groups")
            .WithTags("Groups Management")
            .WithGroupName("GroupsManagement")
            .MapAddChildrenToGroupEndpoint()
            .MapRemoveChildrenFromGroupEndpoint()
            .MapCreateCohortEndpoint()
            .MapCreateGroupEndpoint()
            .MapDeleteGroupEndpoint()
            .MapUpdateGroupEndpoint();
    }
}