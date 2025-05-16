using FastEndpoints; 
using FluentValidation; 

namespace ALB.Api.UseCases.Endpoints.Admin.Group.CreateGroup;

public class CreateGroupValidation : Validator<CreateGroupRequest>
{
    public CreateGroupValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is Required");
    }
}