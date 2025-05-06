namespace ALB.Api.UseCases.Features.Endpoints.Administrator.User.CreateUser;

public class CreateUserRequest
{
    public string UserName { get; set; }
    public DateTime UserDateofBirth { get; set; }+
    public string UserEmail { get; set; }
}