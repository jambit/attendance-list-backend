using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Admin.Group.CreateGroup;

public class CreateGroupEndpoint : Endpoint<CreateGroupRequest, CreateGroupResponse>
{
    public override void Configure()
    {
        Post("/admin/group/create");
    }

    public override async Task HandleAsync(CreateGroupRequest request, CancellationToken cancellationToken)
    {
        await SendAsync(new CreateGroupResponse
        {
            Id = Guid.Empty,
            Message = "Endpoint is not yet implemented.",
        }, cancellation: cancellationToken);
    }
}