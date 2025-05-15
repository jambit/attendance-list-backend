using FastEndpoints; 
using FluentValidation;

namespace ALB.Api.UseCases.Endpoints.Admin.Child.DeleteChild;

public class DeleteChildValidation : Validator<DeleteChildRequest>
{
    public DeleteChildValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Child Id is Required");
    }
}