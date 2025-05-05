namespace ALB.Api.UseCases.Features.Endpoints.Administrator.Child;

internal static class DeleteChildEndpoint
{
    internal static void MapDeleteChildEndpoint(this RouteGroupBuilder app)
    {
        app.MapDelete("/{id:guid}", async (Guid id, IExamplesAdapter adapter) => //hier muss noch ein adpater gemacht werden -> nur example bisher
        {
            await adapter.DeleteExampleAsync(id); // muss auch nochmal neu definiert werden (Platzhalter)
            return Results.NoContent();
        })
        .WithOpenApi()
    }
}