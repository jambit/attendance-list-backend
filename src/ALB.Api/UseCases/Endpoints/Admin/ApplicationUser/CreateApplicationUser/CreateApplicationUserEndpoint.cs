using FastEndpoints; 

namespace ALB.Api.UseCases.Endpoints.Admin.ApplicationUser.CreateApplicationUser;

public class CreateApplicationUserEndpoint : EndpointWithoutRequest<CreateApplicationUserResponse>
{
    public override void Configure()
    {
        Post("/api/users");
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        await SendAsync(new CreateApplicationUserResponse
        {
            Id = Guid.Empty,
            Message = "not implemented",
        }, cancellation: cancellationToken);
    }
}