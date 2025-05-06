using ALB.Api.UseCases.ExampleFeatures.Endpoints.Create;
using FastEndpoints;
using FluentValidation;
using ALB.Api.Endpoints.GroupAdmin.DeleteChildren;

namespace ALB.Api.Endpoints.GroupAdmin.DeleteChildren;

public class DeleteChildrenValidator : Validator<DeleteChildrenRequest>
{
    public DeleteChildrenValidator()
    {
        RuleFor(x => x.ChildName)
            .NotEmpty().WithMessage("Name of Child cannot be empty");

        RuleFor(x => x.GroupName)
            .NotEmpty().WithMessage("Group name cannot be empty");
    }
    
}