using FastEndpoints;
using FluentValidation;

namespace ALB.Api.UseCases.Features.Endpoints.Administrator.User.CreateUser;

public class CreateUserValidator : Validator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Name is required");
        
        RuleFor(x => x.UserDateofBirth)
            .NotEmpty().WithMessage("Date of birth is required");
        
        RuleFor(x => x.UserEmail)
            .NotEmpty().WithMessage("Email is required");
    }
}