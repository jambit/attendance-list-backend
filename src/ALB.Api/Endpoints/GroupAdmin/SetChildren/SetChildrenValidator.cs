using FastEndpoints;
using FluentValidation;
using ALB.Api.Endpoints.GroupAdmin.SetChildren;

namespace ALB.Api.Endpoints.GroupAdmin.SetChildren;

public class SetChildrenValidator : Validator<SetChildrenRequest>
{
    public SetChildrenValidator()
    {
        RuleFor(x => x.ChildName)
            .NotEmpty().WithMessage("Name of Child cannot be empty");

        RuleFor(x => x.GroupName)
            .NotEmpty().WithMessage("Group name cannot be empty");
    }
}