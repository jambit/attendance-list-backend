using ALB.Domain.Values;
using FastEndpoints;
using ALB.Domain.Entities;
using ALB.Domain.Repositories;
using NodaTime;

namespace ALB.Api.UseCases.Endpoints.Children;

public class CreateChildEndpoint(IChildRepository childRepository) : Endpoint<CreateChildRequest, CreateChildResponse>
{
    public override void Configure()
    {
        Post("/api/children");
        Policies(SystemRoles.AdminPolicy);
    }


    public override async Task HandleAsync(CreateChildRequest request, CancellationToken ct)
    {
        var child = new Child
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = LocalDate.FromDateTime(request.ChildDateOfBirth),
            GroupId = request.GroupId,
        };

        var createdChild = await childRepository.CreateAsync(child);

        var response = new CreateChildResponse(createdChild.Id, $"Created child {createdChild.FirstName} {createdChild.LastName}");

        await SendAsync(response, cancellation: ct);
    }
}

public record CreateChildRequest(string FirstName, string LastName, DateTime ChildDateOfBirth, Guid GroupId);

public record CreateChildResponse(Guid Id, string Message);