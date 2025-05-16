using FastEndpoints;
using FluentValidation;

namespace ALB.Api.UseCases.Endpoints.Admin.Role.DeleteUserRole;

public class DeleteUserRoleValidation : Validator<DeleteUserRoleRequest>
{
    public DeleteUserRoleValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }
}