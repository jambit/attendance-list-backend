using FastEndpoints;
using FluentValidation;

namespace ALB.Api.UseCases.Endpoints.Admin.Child.ReadChild;

public class ReadChildValidation: Validator<ReadChildRequest>
{
    public ReadChildValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }
}