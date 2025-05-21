using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Admin.Child.CreateChild;

public class CreateChildEndpoint : EndpointWithoutRequest<CreateChildResponse>
{
    public override void Configure()
    {
        Post("/api/children");
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        await SendAsync(new CreateChildResponse
        {
            Id = Guid.Empty,
            Message = "Endpoint is not yet implemented.",
        }, cancellation: cancellationToken);
    }
}