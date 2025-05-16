using System.ComponentModel.DataAnnotations;
using FastEndpoints;
using FluentValidation;

namespace ALB.Api.UseCases.Endpoints.Admin.ApplicationUser.ReadApplicationUser;

public class ReadApplicationUserValidation : Validator<ReadApplicationUserRequest>
{
    public ReadApplicationUserValidation()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Id is required");   
    }
}