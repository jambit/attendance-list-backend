using FastEndpoints;
using FluentValidation;

namespace ALB.Api.UseCases.Endpoints.GroupAdmin.UpdateChild;

public class UpdateChildValidator : Validator<UpdateChildRequest>
{
    public UpdateChildValidator()
    {
        RuleFor(x => x.ChildName)
            .NotEmpty().WithMessage("Name of child cannot be empty");
        RuleFor(x => x.GroupName)
            .NotEmpty().WithMessage("Name of group cannot be empty");
    }
}