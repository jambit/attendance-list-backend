using ALB.Infrastructure.Persistence.Examples.Adapters;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ALB.Api.UseCases.ExampleFeatures.Endpoints.Create;

internal static class CreateExampleEndpoints
{
    internal static void MapCreateExampleEndpoint(this RouteGroupBuilder app)
    {
        app.MapPost("/", async Task<Results<Ok<ExampleCreatedResponse>, BadRequest>> (CreateExampleRequest request, IExamplesAdapter adapter) =>
            {
                var result = await adapter.CreateExampleAsync(request.Name);
                return TypedResults.Ok(new ExampleCreatedResponse(result.Id, result.Name));
            }) //.RequireAuthorization(SystemRoles.AdminPolicy)
            .WithOpenApi();
    }
}

public record CreateExampleRequest(string Name);
public record ExampleCreatedResponse(Guid Id, string Name);