using FastEndpoints;

namespace ALB.Api.Endpoints.GroupAdmin.UpdateChildren;

public class UpdateChildrenEndpoint : Endpoint<UpdateChildrenRequest, UpdateChildrenResponse>
{
    public override void Configure()
    {
        Put("/group-admin/update-children");
        AllowAnonymous();
    }

    public async override Task HandleAsync(UpdateChildrenRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"{request.ChildName} was moved to group {request.GroupName}");

        await SendAsync(new UpdateChildrenResponse
        {
            Message = $"Group {request.GroupName} was successfully moved to group {request.GroupName}"
        });
    }
}