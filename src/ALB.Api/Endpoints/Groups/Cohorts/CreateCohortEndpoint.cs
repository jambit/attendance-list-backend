using ALB.Domain.Entities;
using ALB.Domain.Repositories;
using FastEndpoints;

namespace ALB.Api.Endpoints.Groups.Cohorts;

public class CreateCohortEndpoint(ICohortRepository cohortRepo)
    : Endpoint<CreateCohortRequest, CreateCohortResponse>
{
    public override void Configure()
    {
        Post("/api/groups/{groupId:guid}/cohorts");
        Policies("AdminPolicy");
    }

    public override async Task HandleAsync(CreateCohortRequest request, CancellationToken ct)
    {
        var groupId = Route<Guid>("groupId");

        var exists = await cohortRepo.ExistsAsync(request.CreationYear, groupId, request.GradeId);
        if (exists)
        {
            AddError("Cohort with the same year, group and grade already exists.");
            await SendErrorsAsync(400, ct);
            return;
        }

        var cohort = new Cohort
        {
            Id = Guid.NewGuid(),
            CreationYear = request.CreationYear,
            GroupId = groupId,
            GradeId = request.GradeId
        };

        var created = await cohortRepo.CreateAsync(cohort);
        await SendAsync(new CreateCohortResponse(created.Id, "Cohort created successfully."), cancellation: ct);
    }
}

public record CreateCohortRequest(int CreationYear, Guid GradeId);

public record CreateCohortResponse(Guid Id, string Message);