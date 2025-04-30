using ALB.Api.UseCases.ExampleFeatures.Endpoints.Create;
using ALB.Api.UseCases.ExampleFeatures.Endpoints.Delete;

namespace ALB.Api.UseCases.ExampleFeatures.Endpoints;

internal static class ExampleEndpoints
{
    internal static void MapExampleEndpoints(this IEndpointRouteBuilder groupBuilder)
    {
        var examplesGroup = groupBuilder.MapGroup("examples");

        examplesGroup.MapCreateExampleEndpoint();
        examplesGroup.MapDeleteExampleEndpoint();
    }
}