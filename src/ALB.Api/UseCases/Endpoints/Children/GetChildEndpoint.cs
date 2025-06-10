using ALB.Api.Extensions;
using ALB.Domain.Values;
using FastEndpoints;
using ALB.Infrastructure.Persistence.Repositories.Admin;
using ALB.Domain.Entities;

namespace ALB.Api.UseCases.Endpoints.Children;

public class GetChildEndpoint(IChildRepository childRepository) : EndpointWithoutRequest<GetChildResponse>
{
    public override void Configure()
    {
        Get("/api/children/{childId:guid}");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var childId = Route<Guid>("childId");

        var child = await childRepository.GetByIdAsync(childId);

        if (child is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var response = new GetChildResponse(
            child.Id,
            child.FirstName,
            child.LastName,
            DateOnlyExtensions.ToDateTime(child.DateOfBirth)
        );

        await SendAsync(response, cancellation: ct);
    }
}

public record GetChildResponse(Guid Id, string FirstName, string LastName, DateTime DateOfBirth);