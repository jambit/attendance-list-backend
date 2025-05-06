using FastEndpoints;
using FluentValidation;

namespace ALB.Api.Features.Endpoints.GroupAdmin.UpdateChildren;

public class UpdateChildrenValidator : Validator<UpdateChildrenRequest>
{
    public UpdateChildrenValidator()
    {
        RuleFor(x => x.ChildName)
            .NotEmpty().WithMessage("Name of child cannot be empty");
        RuleFor(x => x.GroupName)
            .NotEmpty().WithMessage("Name of group cannot be empty");
    }
}