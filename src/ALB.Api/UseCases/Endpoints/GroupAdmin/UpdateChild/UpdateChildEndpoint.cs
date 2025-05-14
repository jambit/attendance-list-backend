
using FastEndpoints;


namespace ALB.Api.UseCases.Endpoints.GroupAdmin.UpdateChild;

public class UpdateChildEndpoint : Endpoint<UpdateChildRequest, UpdateChildResponse>
{
    public override void Configure()
    {
        Put("/group-admin/update-children");
        AllowAnonymous();
    }

    public async override Task HandleAsync(UpdateChildRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"{request.ChildName} was moved to group {request.GroupName}");

        await SendAsync(new UpdateChildResponse
        {
            Message = $"Group {request.GroupName} was successfully moved to group {request.GroupName}"
        });
    }
}