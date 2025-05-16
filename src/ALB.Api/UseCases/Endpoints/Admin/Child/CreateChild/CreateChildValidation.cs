using FluentValidation;
using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Admin.Child.CreateChild;


public class CreateChildValidation : Validator<CreateChildRequest>
{
    public CreateChildValidation()
    {
        RuleFor(x => x.ChildName)
            .NotEmpty().WithMessage("Child name is required");

        RuleFor(x=> x.ChildDoB)
            .NotEmpty().WithMessage("Child Date of Birth is required");
    }
}