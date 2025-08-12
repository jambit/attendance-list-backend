using ALB.Domain.Repositories;
using ALB.Domain.Values;
using FastEndpoints;

namespace ALB.Api.Endpoints.Children;

public class UpdateChildEndpoint(IChildRepository childRepository) : Endpoint<UpdateChildRequest, UpdateChildResponse>
{
    public override void Configure()
    {
        Put("/api/children/{childId:guid}");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(UpdateChildRequest request, CancellationToken cancellationToken)
    {
        var childId = Route<Guid>("childId");

        var existingChild = await childRepository.GetByIdAsync(childId);
        if (existingChild == null)
        {
            AddError($"Child with ID {childId} not found.");
            ThrowIfAnyErrors();
        }

        existingChild.FirstName = request.ChildFirstName;

        await childRepository.UpdateAsync(existingChild);

        await SendAsync(new UpdateChildResponse($"Updated child with ID: {childId}"),
            cancellation: cancellationToken);
    }
}

public record UpdateChildRequest(string ChildFirstName, string ChildLastName, DateTime ChildDateOfBirth);

public record UpdateChildResponse(string Message);