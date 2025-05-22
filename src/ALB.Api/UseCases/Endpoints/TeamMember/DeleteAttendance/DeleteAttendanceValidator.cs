using FastEndpoints;
using FluentValidation;

namespace ALB.Api.UseCases.Endpoints.TeamMember.DeleteAttendance;

public class DeleteAttendanceValidator : Validator<DeleteAttendanceRequest>
{
    public DeleteAttendanceValidator()
    {
        RuleFor(x => x.ChildId)
            .NotEmpty().WithMessage("Name of child cannot be empty.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date cannot be empty.");

    }
}
