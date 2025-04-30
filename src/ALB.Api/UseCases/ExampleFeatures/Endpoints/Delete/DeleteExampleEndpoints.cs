using ALB.Infrastructure.Persistence.Examples.Adapters;

namespace ALB.Api.UseCases.ExampleFeatures.Endpoints.Delete;

internal static class DeleteExampleEndpoints
{
    internal static void MapDeleteExampleEndpoint(this RouteGroupBuilder app)
    {
        app.MapDelete("/{id:guid}", async (Guid id, IExamplesAdapter adapter) =>
            {
                await adapter.DeleteExampleAsync(id);
                return Results.NoContent();
            }) //.RequireAuthorization(SystemRoles.AdminPolicy)
           .WithOpenApi();
    }
}
