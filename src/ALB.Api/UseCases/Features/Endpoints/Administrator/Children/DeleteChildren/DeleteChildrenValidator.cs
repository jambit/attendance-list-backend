using FluentValidation;
using FastEndpoints

namespace ALB.Api.UseCases.Features.Endpoints.Administrator.Children.DeleteChildren;

public class DeleteChildrenValidator : Validator<DeleteChildrenRequest>
{
    public DeleteChildrenValidator
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Child Id is Required");
    }
}