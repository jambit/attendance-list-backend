using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Admin.ApplicationUser.GetUser;

public class GetUserEndpoint : EndpointWithoutRequest<GetUserResponse>
{
    public override void Configure()
    {
        Get("/api/users/{id:Guid}");
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var id = Route<Guid>("id");

        await SendAsync(new GetUserResponse("not implemented yet"),
            cancellation: cancellationToken);
    }
}