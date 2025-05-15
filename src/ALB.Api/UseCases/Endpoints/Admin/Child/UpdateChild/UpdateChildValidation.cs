using FastEndpoints; 
using FluentValidation;

namespace ALB.Api.UseCases.Endpoints.Admin.Child.UpdateChild;

public class UpdateChildValidation: Validator<UpdateChildRequest>
{
    public UpdateChildValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }
}