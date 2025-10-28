using ALB.Domain.Entities;
using ALB.Domain.Repositories;
using ALB.Domain.Values;

namespace ALB.Api.Endpoints.Groups.Cohorts;

internal static class CreateCohortEndpoint
{
    internal static IEndpointRouteBuilder MapCreateCohortEndpoint(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("/{groupId:guid}/cohorts", async (Guid groupId, CreateCohortRequest request, ICohortRepository cohortRepo, CancellationToken ct) =>
        {
            var exists = await cohortRepo.ExistsAsync(request.CreationYear, groupId, request.GradeId);
            if (exists)
            {
                return Results.BadRequest("Cohort with the same year, group and grade already exists.");
            }

            var cohort = new Cohort
            {
                Id = Guid.NewGuid(),
                CreationYear = request.CreationYear,
                GroupId = groupId,
                GradeId = request.GradeId
            };

            var created = await cohortRepo.CreateAsync(cohort);
            
            return Results.Ok(new CreateCohortResponse(created.Id));
        }).WithName("CreateCohort")
        .WithOpenApi()
        .RequireAuthorization(SystemRoles.AdminPolicy);
        
        return routeBuilder;
    }
}

public record CreateCohortRequest(int CreationYear, Guid GradeId);

public record CreateCohortResponse(Guid Id);