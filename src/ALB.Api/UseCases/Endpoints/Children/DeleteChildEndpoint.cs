using ALB.Domain.Values;
using FastEndpoints;
using ALB.Infrastructure.Persistence.Repositories.Admin;

namespace ALB.Api.UseCases.Endpoints.Children;

public class DeleteChildEndpoint : EndpointWithoutRequest<DeleteChildResponse>
{
    private readonly IChildRepository _childRepository;

    public DeleteChildEndpoint(IChildRepository childRepository)
    {
        _childRepository = childRepository;
    }

    public override void Configure()
    {
        Delete("/api/children/{childId:guid}");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var childId = Route<Guid>("childId");

        var child = await _childRepository.GetByIdAsync(childId);

        if (child is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await _childRepository.DeleteAsync(childId);

        await SendAsync(new DeleteChildResponse("Child successfully deleted"), cancellation: ct);
    }
}

public record DeleteChildResponse(string Message);