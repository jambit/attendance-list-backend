using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ALB.Api.UseCases.ExampleFeatures.Endpoints.Administrator.Child;

internal static class GetChildEndpoint
{
    internal static void MapGetChildEndpoint(this RouteGroupBuilder app)
    {
        app.MapGet("/{id:guid}", async Task<Results<Ok<GetChildResponse>, NotFound>> (Guid id, IExampleAdapter adapter) =>
            //hier muss auch noch ein adpater gebaut werden, stat dem example
            {
                var child = await adapter.GetExamplesAsync(id); 
                if (child is null)
                    return TypedResults.NotFound();

                return TypedResults.Ok(new GetChildResponse(
                    child.Id,
                    child.FirstName,
                    child.LastName,
                    child.BirthDate
                ));
            })
            .WithOpenApi();
    }
}

public record GetChildResponse(Guid Id, string FirstName, string LastName, DateTime BirthDate);