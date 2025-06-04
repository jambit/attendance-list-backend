using ALB.Domain.Values;
using FastEndpoints;
using ALB.Infrastructure.Persistence.Repositories.Admin;
using ALB.Domain.Entities;

namespace ALB.Api.UseCases.Endpoints.Children;

public class CreateChildEndpoint : Endpoint<CreateChildRequest, CreateChildResponse>
{
    private readonly IChildRepository _childRepository;
    
    public CreateChildEndpoint(IChildRepository childRepository)
    {
        _childRepository = childRepository;
    }
    public override void Configure()
    {
        Post("/api/children");
        Policies(SystemRoles.AdminPolicy);
    }


    public override async Task HandleAsync(CreateChildRequest request, CancellationToken ct)
    {
        var child = new Child
        {
            Id = Guid.NewGuid(),
            FirstName = request.ChildFirstName,
            LastName = request.ChildLastName,
            DateOfBirth = request.ChildDateOfBirth,
            
        };

        var createdChild = await _childRepository.CreateAsync(child);

        var response = new CreateChildResponse(createdChild.Id, $"Created child {createdChild.FirstName} {createdChild.LastName}");

        await SendAsync(response, cancellation: ct);
    }
}

public record CreateChildRequest(string ChildFirstName, string ChildLastName, DateTime ChildDateOfBirth);

public record CreateChildResponse(Guid Id, string Message);