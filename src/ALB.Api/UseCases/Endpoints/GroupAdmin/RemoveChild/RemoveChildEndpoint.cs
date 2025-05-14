using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.GroupAdmin.RemoveChild;

public class RemoveChildEndpoint : Endpoint<RemoveChildRequest, RemoveChildResponse>
{
    public override void Configure()
    {
        Delete("/groupadmin/deletechildren");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RemoveChildRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Child {request.ChildName} was deleted from group {request.GroupName}");

        await SendAsync(new RemoveChildResponse
        {
            Message = $"Child {request.ChildName} was successfully deleted from group {request.GroupName}"
        });
    }
}