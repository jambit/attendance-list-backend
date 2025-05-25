using ALB.Api.UseCases.Endpoints.Admin.Child.GetChild;
using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Admin.Child.ReadChild;

public class GetChildEndpoint : EndpointWithoutRequest<GetChildResponse>
{
    public override void Configure()
    {
        Get("/api/children/{id:guid}");
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {

        var id = Route<Guid>("id");

        await SendAsync(new GetChildResponse("Not implemented yet"),
            cancellation: cancellationToken);
    }
}