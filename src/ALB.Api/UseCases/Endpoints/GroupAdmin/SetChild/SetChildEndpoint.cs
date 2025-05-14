using FastEndpoints;
    

namespace ALB.Api.UseCases.Endpoints.GroupAdmin.SetChild;

public class SetChildEndpoint : Endpoint<SetChildRequest, SetChildResponse>
{
    public override void Configure()
    {
        Post("api/groupadmin/setchildren");
        AllowAnonymous();
    }

    public override async Task HandleAsync(SetChildRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"{request.GroupName} was set to group {request.GroupName}");

        await SendAsync(new SetChildResponse()
        {
            Message = $"Group {request.GroupName} was successfully set to group {request.GroupName}"
        });
    }
}