using System.Data;
using FluentValidation;
using FastEndpoints

namespace ALB.Api.UseCases.Features.Endpoints.Administrator.Children.CreateChildren;

public class CreateChildrenValidator : Validator<CreateChildrenRequest>
{
    public CreateChildrenValidator()
    {
        RuleFor(x => x.ChildName)
            .NotEmpty().WithMessage("Child name is required");
        
        RuleFor(x=> x.ChildDoB)
            .NotEmpty().WithMessage("Child Date of Birth is required");
    }
}