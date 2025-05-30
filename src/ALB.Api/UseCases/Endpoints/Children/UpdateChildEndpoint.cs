using ALB.Domain.Values;
using FastEndpoints;
using ALB.Infrastructure.Persistence.Adapters.Admin;

namespace ALB.Api.UseCases.Endpoints.Children;

public class UpdateChildEndpoint : Endpoint<UpdateChildRequest, UpdateChildResponse>
{
    private readonly IChildAdapter _childAdapter;

    public UpdateChildEndpoint(IChildAdapter childAdapter)
    {
        _childAdapter = childAdapter;
    }
    public override void Configure()
    {
        Put("/api/children/{childId:guid}");
        Policies(SystemRoles.AdminPolicy);
    }
    
    public override async Task HandleAsync(UpdateChildRequest request, CancellationToken cancellationToken)
    {
        var childId = Route<Guid>("childId");
        
        var existingChild = await _childAdapter.GetByIdAsync(childId);
        if (existingChild == null)
        {
            AddError($"Child with ID {childId} not found.");
            ThrowIfAnyErrors();
        }
        
        existingChild.FirstName = request.ChildFirstName;
        
        await _childAdapter.UpdateAsync(existingChild);

        await SendAsync(new UpdateChildResponse($"Updated child with ID: {childId}"),
            cancellation: cancellationToken);
    }
}

public record UpdateChildRequest(string ChildFirstName, string ChildLastName, DateTime ChildDateOfBirth);

public record UpdateChildResponse(string Message);