using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Admin.ApplicationUser.ReadApplicationUser;

public class ReadApplicationUserEndpoint : Endpoint<ReadApplicationUserRequest, ReadApplicationUserResponse>
{
    public override void Configure()
    {
        Get("/admin/application-user/read-application-user");
    }

    public override async Task HandleAsync(ReadApplicationUserRequest request, CancellationToken cancellationToken)
    {
        await SendAsync(new ReadApplicationUserResponse
        {
            Message = "not implemented",
        }, cancellation: cancellationToken);
    }
}