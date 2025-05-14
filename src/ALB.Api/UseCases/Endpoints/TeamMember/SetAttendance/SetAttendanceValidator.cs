using FastEndpoints;
using FluentValidation;

namespace ALB.Api.UseCases.Endpoints.TeamMember.SetAttendance;

public class SetAttendanceValidator : Validator<SetAttendanceRequest>
{
    public SetAttendanceValidator()
    {
        RuleFor(x => x.ChildName)
            .NotEmpty().WithMessage("Name of child cannot be empty");

        RuleFor(x => x.Status)
            .Must(s => new[] { "present", "absent", "excused" }.Contains(s.ToLower()))
            .WithMessage("Invalid status. Permitted: present, absent, excused.");
        
    }
}