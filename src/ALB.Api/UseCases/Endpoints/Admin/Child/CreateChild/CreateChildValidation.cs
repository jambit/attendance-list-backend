using FastEndpoints;
using FluentValidation;

namespace ALB.Api.UseCases.Endpoints.Admin.Child.CreateChild;

public class CreateChildValidation : Validator<CreateChildRequest>
{
    public CreateChildValidation()
    {
        RuleFor(x => x.ChildName)
            .NotEmpty().WithMessage("Name is required");
    }
}