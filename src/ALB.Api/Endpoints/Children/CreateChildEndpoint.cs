using ALB.Domain.Entities;
using ALB.Domain.Repositories;
using ALB.Domain.Values;

using NodaTime;

namespace ALB.Api.Endpoints.Children;

internal static class CreateChildEndpoint
{
    internal static RouteGroupBuilder AddCreateChildEndpoint(this RouteGroupBuilder builder)
    {
        builder.MapPost("/", async (CreateChildRequest request, IChildRepository childRepository) =>
        {
            var child = new Child
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth
            };

            var createdChild = await childRepository.CreateAsync(child);

            return Results.Ok(new CreateChildResponse(createdChild.Id, createdChild.FirstName,
                createdChild.LastName, createdChild.DateOfBirth));
        }).WithName("CreateChild")
            .WithOpenApi()
            .RequireAuthorization(policy => policy.RequireRole(SystemRoles.Admin));
        return builder;
    }
}

public record CreateChildRequest(string FirstName, string LastName, LocalDate DateOfBirth);

public record CreateChildResponse(Guid Id, string FirstName, string LastName, LocalDate DateOfBirth);