using FluentValidation;
using RealEstate.Application.Features.Auth.Validators;

namespace RealEstate.Application.Features.Auth.Commands.UpdatePassword;

public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
{
    public UpdatePasswordCommandValidator()
    {
        RuleFor(x => x.Request.OldPassword)
            .NotEmpty().WithMessage("Old password is required.");

        RuleFor(x => x.Request.NewPassword)
            .StrongPassword()
            .NotEqual(x => x.Request.OldPassword).WithMessage("New password must be different from old password.");

        RuleFor(x => x.Request.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password is required.")
            .Equal(x => x.Request.NewPassword).WithMessage("Confirm password must match new password.");
    }
}
