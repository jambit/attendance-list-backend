using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Admin.Child.CreateChild;

public class CreateChildEndpoint : Endpoint<CreateChildRequest, CreateChildResponse>
{
    public override void Configure()
    {
        Post("/admin/child/create-children");
    }

    public override async Task HandleAsync(CreateChildRequest request, CancellationToken cancellationToken)
    {
        await SendAsync(new CreateChildResponse
        {
            Id = Guid.Empty,
            Message = "Endpoint is not yet implemented.",
        }, cancellation: cancellationToken);
    }
}