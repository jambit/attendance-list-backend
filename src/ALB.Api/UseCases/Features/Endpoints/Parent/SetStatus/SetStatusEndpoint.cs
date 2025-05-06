using FastEndpoints;
using ALB.Api.Endpoints.Parent;

namespace ALB.Api.Endpoints.Parent.SetStatus;

public class SetStatusEndpoint : Endpoint<SetStatusRequest, SetStatusResponse>
{
    public override void Configure()
    {
        Post("/api/v1/parent/set-status");
        AllowAnonymous();
        
    }

    public override async Task HandleAsync(SetStatusRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"{request.ChildName} was reported as {request.Status}");

        await SendAsync(new SetStatusResponse
        {
            Message = $"Status of {request.ChildName} was succesfully saved as {request.Status}"
        });
    }
}