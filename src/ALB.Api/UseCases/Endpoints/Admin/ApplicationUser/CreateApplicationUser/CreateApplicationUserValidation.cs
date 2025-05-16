using System.Data;
using FastEndpoints;
using FluentValidation;

namespace ALB.Api.UseCases.Endpoints.Admin.ApplicationUser.CreateApplicationUser;

public class CreateApplicationUserValidation : Validator<CreateApplicationUserRequest>
{
    public CreateApplicationUserValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");
    }
}