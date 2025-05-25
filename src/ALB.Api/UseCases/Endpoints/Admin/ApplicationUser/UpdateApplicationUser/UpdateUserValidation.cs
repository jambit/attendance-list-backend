using FastEndpoints;
using FluentValidation;

namespace ALB.Api.UseCases.Endpoints.Admin.ApplicationUser.UpdateApplicationUser;

public class UpdateUserValidation : Validator<UpdateUserRequest>
{
    public UpdateUserValidation()
    {
        RuleFor(x => x.UserName)
            .NotNull().WithMessage("Name is required");
    }
}