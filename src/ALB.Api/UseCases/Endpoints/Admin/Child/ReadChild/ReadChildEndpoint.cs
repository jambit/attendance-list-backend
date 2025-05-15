using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Admin.Child.ReadChild;

public class ReadChildEndpoint : Endpoint<ReadChildRequest, ReadChildResponse>
{
    public override void Configure()
    {
        Get("/admin/children/read-children");
    }

    public override async Task HandleAsync(ReadChildRequest request, CancellationToken cancellationToken)
    {
        await SendAsync(new ReadChildResponse
        {
            Id = request.Id, // Platzhalterwert
            Name = "Not implemented",
            DateofBirth = DateTime.MinValue
        }, cancellation: cancellationToken);
    }
}