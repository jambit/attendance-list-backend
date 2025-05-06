using FastEndpoints;
using ALB.Api.Features.Endpoints.GroupAdmin.SetChildren;

namespace ALB.Api.Features.Endpoints.GroupAdmin.SetChildren;

public class SetChildrenEndpoint : Endpoint<SetChildrenRequest, SetChildrenResponse>
{
    public override void Configure()
    {
        Post("api/groupadmin/setchildren");
        AllowAnonymous();
    }

    public override async Task HandleAsync(SetChildrenRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"{request.GroupName} was set to group {request.GroupName}");

        await SendAsync(new SetChildrenResponse()
        {
            Message = $"Group {request.GroupName} was successfully set to group {request.GroupName}"
        });
    }
}