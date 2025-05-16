using FastEndpoints;
using FluentValidation;

namespace ALB.Api.UseCases.Endpoints.Admin.Group.DeleteGroup;

public class DeleteGroupValidation : Validator<DeleteGroupRequest>
{
    public DeleteGroupValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Group Id is Required");
    }
}