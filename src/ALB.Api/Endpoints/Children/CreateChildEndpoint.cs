using ALB.Domain.Entities;
using ALB.Domain.Repositories;
using ALB.Domain.Values;
using FastEndpoints;
using NodaTime;

namespace ALB.Api.Endpoints.Children;

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
            DateOfBirth = request.DateOfBirth
        };

        var createdChild = await childRepository.CreateAsync(child, ct);

        await SendOkAsync(
            new CreateChildResponse(createdChild.Id, createdChild.FirstName, createdChild.LastName,
                createdChild.DateOfBirth), ct);
    }
}

public record CreateChildRequest(string FirstName, string LastName, LocalDate DateOfBirth);

public record CreateChildResponse(Guid Id, string FirstName, string LastName, LocalDate DateOfBirth);