using FastEndpoints;

namespace ALB.Api.UseCases.Features.Endpoints.Administrator.User.DeleteUser;

public class DeleteUserEndpoint : Endpoint<DeleteUserRequest, DeleteUserResponse>
{
    public override void Configure()
    {
        Delete("/admin/user/delete-user");
    }

    public override async Task HandleAsync(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        var user = request.Id;  //Datenbankzugriff
        
        //Remove User von Datenbank
        
        await SendAsync(new DeleteUserResponse
        {
            Message = $"User {user.ToString()} was deleted successfully."
        }cancellation: cancellationToken)
    }
}