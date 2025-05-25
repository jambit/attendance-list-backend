using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Admin.Child.CreateChild;

public class CreateChildEndpoint : Endpoint<CreateChildRequest, CreateChildResponse>
{
    public override void Configure()
    {
        Post("/api/children");
    }


    public override async Task HandleAsync(CreateChildRequest request, CancellationToken ct)
    {
        var newId = Guid.NewGuid();

        var response = new CreateChildResponse(newId, $"Created child {request.ChildName}");

        await SendAsync(response, cancellation: ct);
    }
}