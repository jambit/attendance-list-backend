using FastEndpoints;
using FluentValidation;

namespace ALB.Api.UseCases.Endpoints.GroupAdmin.RemoveChild;

public class RemoveChildValidator : Validator<RemoveChildRequest>
{
    public RemoveChildValidator()
    {
        RuleFor(x => x.ChildName)
            .NotEmpty().WithMessage("Name of Child cannot be empty");

        RuleFor(x => x.GroupName)
            .NotEmpty().WithMessage("Group name cannot be empty");
    }

}