namespace ALB.Api.UseCases.Endpoints.Authentication.Logout;

public class LogoutResponse
{
    public string Message { get; set; } = string.Empty;
    public DateTime LoggedOutAt { get; set; }
}