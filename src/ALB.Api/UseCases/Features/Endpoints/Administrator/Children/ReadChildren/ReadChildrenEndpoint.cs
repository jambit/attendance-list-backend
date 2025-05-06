using ALB.Api.UseCases.Features.Endpoints.Administrator.Children.CreateChildren;
using FastEndpoints;

namespace ALB.Api.UseCases.Features.Endpoints.Administrator.Children.ReadChildren;

public class ReadChildrenEndpoint : Endpoint<ReadChildrenRequest, ReadChildrenResponse>
{
    public override void Configure()
    {
        Get("/admin/children/read-children");
    }
    
    public override async Task HandleAsync(ReadChildrenRequest request, CancellationToken cancellationToken)
    {
        var child = request.Id;// Datenbankzugriff
            
        await SendAsync(new ReadChildrenResponse
        {
            Id = child.id,
            Name = child.Name,
            DateofBirth = child.DateofBirth
        } cancellation: cancellationToken);
    }
}