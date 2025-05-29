using ALB.Domain.Values;
using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Children;

public class CreateChildEndpoint : Endpoint<CreateChildRequest, CreateChildResponse>
{
    public override void Configure()
    {
        Post("/api/children");
        Policies(SystemRoles.AdminPolicy);
    }


    public override async Task HandleAsync(CreateChildRequest request, CancellationToken ct)
    {
        var newId = Guid.NewGuid();

        var response = new CreateChildResponse(newId, $"Created child {request.ChildName}");

        await SendAsync(response, cancellation: ct);
    }
}

public record CreateChildRequest(string ChildName);

public record CreateChildResponse(Guid Id, string Message);