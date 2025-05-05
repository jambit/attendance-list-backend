using Microsoft.AspNetCore.Identity;

namespace ALB.Domain.Identity;

public class DummyEmailSender : IEmailSender<ApplicationUser>
{
    public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
    {
        throw new NotImplementedException("Email sending functionality is not implemented yet.");
    }

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
    {
        throw new NotImplementedException("Email sending functionality is not implemented yet.");
    }

    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
    {
        throw new NotImplementedException("Email sending functionality is not implemented yet.");
    }
}