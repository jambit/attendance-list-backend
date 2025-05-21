using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.GroupAdmin.RemoveChild;

public class RemoveChildEndpoint : Endpoint<RemoveChildRequest, RemoveChildResponse>
{
    public override void Configure()
    {
        Delete("api/children/{childid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RemoveChildRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Child {request.ChildId} was deleted from group ");

        await SendAsync(new RemoveChildResponse
        {
            Message = $"Child {request.ChildId} was successfully deleted from group "
        });
    }
}