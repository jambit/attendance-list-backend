using ALB.Infrastructure.Persistence.Examples.Adapters;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ALB.Api.UseCases.Features.Endpoints.Administrator.Child;

internal static class CreateChildEndpoint
{
    internal static void MapCreateChildEndpoint(this RouteGroupBuilder app)
    {
        app.MapPost("/",
            async Task<Results<Ok<ExampleCreatedResponse>, BadRequest>> (CreateExampleRequest request,
                IExamplesAdapter adapter) =>
            {
                var result = await adapter.CreateExampleAsync(request.Name);
                return TypedResults.Ok(new ExampleCreatedResponse(result.Id, result.Name));
            })
            .WithOpenApi()
    }
}


public record CreateExampleRequest(string Name);
public record ExampleCreatedResponse(Guid Id, string Name);