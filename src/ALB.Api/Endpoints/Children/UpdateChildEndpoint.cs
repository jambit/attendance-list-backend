using ALB.Domain.Repositories;
using ALB.Domain.Values;
using FastEndpoints;
using NodaTime;

namespace ALB.Api.Endpoints.Children;

public class UpdateChildEndpoint(IChildRepository childRepository) : Endpoint<UpdateChildRequest>
{
    public override void Configure()
    {
        Put("/api/children/{childId:guid}");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(UpdateChildRequest request, CancellationToken ct)
    {
        var childId = Route<Guid>("childId");

        var existingChild = await childRepository.GetByIdAsync(childId, ct);
        if (existingChild is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        existingChild.FirstName = request.ChildFirstName;
        existingChild.LastName = request.ChildLastName;
        existingChild.DateOfBirth = request.ChildDateOfBirth;

        await childRepository.UpdateAsync(existingChild, ct);

        await SendNoContentAsync(ct);
    }
}

public record UpdateChildRequest(string ChildFirstName, string ChildLastName, LocalDate ChildDateOfBirth);