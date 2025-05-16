using System.Data;
using FastEndpoints;
using FluentValidation;

namespace ALB.Api.UseCases.Endpoints.Admin.Role.SetUserRole;

public class SetUserRoleValidation : Validator<SetUserRoleRequest>
{
    public SetUserRoleValidation()
    {
        RuleFor(x => x.RoleId)
            .NotEmpty().WithMessage("RoleId is required");
    }
}