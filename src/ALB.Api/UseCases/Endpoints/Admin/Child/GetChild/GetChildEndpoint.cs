using ALB.Api.UseCases.Endpoints.Admin.Child.GetChild;
using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Admin.Child.ReadChild;

public class GetChildEndpoint : EndpointWithoutRequest<GetChildResponse>
{
    public override void Configure()
    {
        Get("/api/child/{id:guid}");
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        
        var id = Route<Guid>("id", request);
        
        
        await SendAsync(new GetChildResponse
        {
            Id = id,
            Name = "Not implemented",
            DateofBirth = DateTime.MinValue
        }, cancellation: cancellationToken);
    }
}