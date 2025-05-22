using FastEndpoints;
using FluentValidation;

namespace ALB.Api.UseCases.Endpoints.GroupAdmin.SetChild;

public class SetChildValidator : Validator<SetChildRequest>
{
    public SetChildValidator()
    {
        RuleFor(x => x.ChildId)
            .NotEmpty().WithMessage("Name of Child cannot be empty");

        RuleFor(x => x.GroupId)
            .NotEmpty().WithMessage("Group name cannot be empty");
    }
}