using FastEndpoints; 
using FluentValidation;

namespace ALB.Api.UseCases.Endpoints.Admin.Child.UpdateChild;

public class UpdateChildValidation: Validator<UpdateChildRequest>
{
    public UpdateChildValidation()
    {
        RuleFor(x => x.ChildName)
            .NotEmpty().WithMessage("Name is required");
    }
}