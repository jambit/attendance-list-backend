using FastEndpoints;
using FluentValidation;

namespace ALB.Api.UseCases.Endpoints.Admin.Group.UpdateGroup;

public class PutGroupValidation : Validator<PutGroupRequest>
{
    public PutGroupValidation()
    {
        RuleFor(x => x.GroupName)
            .NotEmpty().WithMessage("Group Name is Required");
    }
}