using ALB.Domain.Values;
using FastEndpoints;
using ALB.Infrastructure.Persistence.Repositories.Admin;

namespace ALB.Api.UseCases.Endpoints.Children;

public class UpdateChildEndpoint : Endpoint<UpdateChildRequest, UpdateChildResponse>
{
    private readonly IChildRepository _childRepository;

    public UpdateChildEndpoint(IChildRepository childRepository)
    {
        _childRepository = childRepository;
    }
    public override void Configure()
    {
        Put("/api/children/{childId:guid}");
        Policies(SystemRoles.AdminPolicy);
    }
    
    public override async Task HandleAsync(UpdateChildRequest request, CancellationToken cancellationToken)
    {
        var childId = Route<Guid>("childId");
        
        var existingChild = await _childRepository.GetByIdAsync(childId);
        if (existingChild == null)
        {
            AddError($"Child with ID {childId} not found.");
            ThrowIfAnyErrors();
        }
        
        existingChild.FirstName = request.ChildFirstName;
        
        await _childRepository.UpdateAsync(existingChild);

        await SendAsync(new UpdateChildResponse($"Updated child with ID: {childId}"),
            cancellation: cancellationToken);
    }
}

public record UpdateChildRequest(string ChildFirstName, string ChildLastName, DateTime ChildDateOfBirth);

public record UpdateChildResponse(string Message);