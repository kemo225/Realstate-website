using FluentValidation;
using RealEstate.Application.Features.Auth.Validators;

namespace RealEstate.Application.Features.Auth.Commands.ResetPassword;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.Request.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is invalid.");

        RuleFor(x => x.Request.Token)
            .NotEmpty().WithMessage("Reset token is required.");

        RuleFor(x => x.Request.NewPassword)
            .StrongPassword();

        RuleFor(x => x.Request.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password is required.")
            .Equal(x => x.Request.NewPassword).WithMessage("Confirm password must match new password.");
    }
}
