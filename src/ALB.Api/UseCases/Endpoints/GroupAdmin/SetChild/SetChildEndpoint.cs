using FastEndpoints;
    

namespace ALB.Api.UseCases.Endpoints.GroupAdmin.SetChild;

public class SetChildEndpoint : Endpoint<SetChildRequest, SetChildResponse>
{
    public override void Configure()
    {
        Post("api/children/{childid}/group/{groupid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(SetChildRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"{request.ChildId} was set to group {request.GroupId}");

        await SendAsync(new SetChildResponse()
        {
            Message = $"Group {request.ChildId} was successfully set to group {request.GroupId}"
        });
    }
}