using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ALB.Api.UseCases.ExampleFeatures.Endpoints.Administrator.Child;

internal static class UpdateChildEndpoint
{
    internal static void MapUpdateChildEndpoint(this RouteGroupBuilder app)
    {
        app.MapPut("/{id:guid}", async Task<Results<NoContent, NotFound>> (
                Guid id,
                UpdateChildRequest request, IExamplesAdapter adapter) => //examples adapter muss noch geöndert werden
            {
                var success = await adapter.UpdateExamplesAsync(id, request.FirstName, request.LastName, request.BirthDate); //auch noch kein adapter gebaut 
                if (!success)
                    return TypedResults.NotFound();

                return TypedResults.NoContent();
            })
            .WithOpenApi();
    }
}

public record UpdateChildRequest(string FirstName, string LastName, DateTime BirthDate);