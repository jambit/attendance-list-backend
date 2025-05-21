using FastEndpoints;
using FluentValidation;

namespace ALB.Api.UseCases.Endpoints.GroupAdmin.RemoveChild;

public class RemoveChildValidator : Validator<RemoveChildRequest>
{
    public RemoveChildValidator()
    {
        RuleFor(x => x.ChildId)
            .NotEmpty().WithMessage("Name of Child cannot be empty");

       
    }

}