using ALB.Api.Endpoints.Children.Absence;

namespace ALB.Api.Endpoints.Children;

internal static class ChildrenEndpointsGroup
{
    internal static IEndpointRouteBuilder MapChildrenEndpointsGroup(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGroup("/api/children")
            .WithTags("Children Management")
            .WithGroupName("ChildrenManagement")
            .AddCreateChildEndpoint()
            .AddDeleteChildEndpoint()
            .AddGetChildEndpoint()
            .AddUpdateChildEndpoint()
            .AddCreateAbsenceForChildEndpoint();

        return routeBuilder;
    }
}