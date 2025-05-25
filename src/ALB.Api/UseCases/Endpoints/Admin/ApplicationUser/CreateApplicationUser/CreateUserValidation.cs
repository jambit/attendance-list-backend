using FastEndpoints;
using FluentValidation;

namespace ALB.Api.UseCases.Endpoints.Admin.ApplicationUser.CreateApplicationUser;

public class CreateUserValidation : Validator<CreateUserRequest>
{
    public CreateUserValidation()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username is required");
    }
}