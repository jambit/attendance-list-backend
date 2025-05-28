using ALB.Domain.Identity;
using ALB.Domain.Values;
using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace ALB.Api.UseCases.Endpoints.Users;

public class CreateUserEndpoint(UserManager<ApplicationUser> userManager) : Endpoint<CreateUserRequest, CreateUserResponse>
{

    public override void Configure()
    {
        Post("/api/users");
        Policies(SystemRoles.AdminPolicy);
    }

    public override async Task HandleAsync(CreateUserRequest request, CancellationToken ct)
    {
        var user = new ApplicationUser
        {
            Email = request.Email,
            UserName = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };
        
        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                AddError(error.Description);
            }
            ThrowIfAnyErrors();
        }

        await SendAsync(new CreateUserResponse(Id: user.Id, Email: user.Email, null, null),
            200, ct);

    }
    
}

public class CreateUserRequestValidator : Validator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required!")
            .EmailAddress().WithMessage("Must be a valid email.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one number.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
    }
}

public record CreateUserRequest(string Email, string Password, string? FirstName, string? LastName);

public record CreateUserResponse(Guid Id, string Email, string? FirstName, string? LastName);