using FastEndpoints;
using FluentValidation;

namespace ALB.Api.UseCases.Features.Endpoints.Administrator.Children.ReadChildren;

public class ReadChildrenValidator : Validator<ReadChildrenRequest>
{
    public ReadChildrenValidator
    {
        RuleFor(x => x.childId)
            .NotEmpty().WithMessage("Child Id is Required");

    }
}