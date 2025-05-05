using FastEndpoints;
using FluentValidation;
using ALB.Api.Endpoints.Parent.SetStatus;
using ALB.Api.UseCases.ExampleFeatures.Endpoints.Create;

namespace ALB.Api.Endpoints.Parent.SetStatus;

public class SetStatusValidator : Validator<SetStatusRequest>
{
    public SetStatusValidator()
    {
        RuleFor(x => x.ChildName)
            .NotEmpty().WithMessage("Name of Child cannot be empty");

        RuleFor(x => x.Status)
            .Must(s => new[] { "present", "absent", "ill", "holiday" }.Contains(s.ToLower()))
            .WithMessage("Invalid status. Allowed: present, absent, ill, holiday.");
    }
}