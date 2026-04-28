using FluentValidation;
using RealEstate.Application.Features.Auth.Validators;

namespace RealEstate.Application.Features.Auth.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Request.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is invalid.");

        RuleFor(x => x.Request.Password)
            .StrongPassword();
    }
}
