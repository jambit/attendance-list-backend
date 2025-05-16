using FastEndpoints;
using FluentValidation;

namespace ALB.Api.UseCases.Endpoints.Admin.ApplicationUser.UpdateApplicationUser;

public class UpdateApplicationUserValidation : Validator<UpdateApplicationUserRequest>
{
    public UpdateApplicationUserValidation()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Id is required");
    }
}