using FastEndpoints;
using FluentValidation;

namespace ALB.Api.UseCases.Endpoints.Admin.Group.UpdateGroup;

public class UpdateGroupValidation : Validator<UpdateGroupRequest>
{
    public UpdateGroupValidation()
    {
        RuleFor(x => x.GroupName)
            .NotEmpty().WithMessage("Group Name is Required");
    }
}