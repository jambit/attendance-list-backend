using FastEndpoints; 
using FluentValidation;

namespace ALB.Api.UseCases.Features.Endpoints.Administrator.User.DeleteUser;

public class DeleteUserValidator : Validator<DeleteUserRequest>
{
    public DeleteUserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is Required");
    }
}