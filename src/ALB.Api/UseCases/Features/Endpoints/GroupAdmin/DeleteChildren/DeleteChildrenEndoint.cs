using ALB.Api.Features.Endpoints.GroupAdmin.DeleteChildren;
using FastEndpoints;

namespace ALB.Api.Features.Endpoints.GroupAdmin.DeleteChildren;

public class DeleteChildrenEndoint : Endpoint<DeleteChildrenRequest, DeleteChildrenResponse>
{
    public override void Configure()
    {
        Delete("/groupadmin/deletechildren");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteChildrenRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Child {request.ChildName} was deleted from group {request.GroupName}");

        await SendAsync(new DeleteChildrenResponse
        {
            Message = $"Child {request.ChildName} was successfully deleted from group {request.GroupName}"
        });
    }
}