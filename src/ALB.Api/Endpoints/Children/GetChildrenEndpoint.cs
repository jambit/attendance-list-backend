using ALB.Api.Extensions;
using ALB.Api.Models;
using ALB.Domain.Repositories;
using ALB.Domain.Values;
using FastEndpoints;
using NodaTime;

namespace ALB.Api.Endpoints.Children;

public class GetChildrenEndpoint(IChildRepository childRepository) : EndpointWithoutRequest<GetChildrenResponse>
{
    public override void Configure()
    {
        Get("/api/children/");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var children = await childRepository.GetAllAsync(ct);

        var response = children.Select(c => c.ToDto()).ToList();

        await SendAsync(new GetChildrenResponse(response), cancellation: ct);
    }
}

public record GetChildrenResponse(IEnumerable<ChildDto> Children);