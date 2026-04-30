using FluentValidation;

namespace RealEstate.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.Request.RefreshToken)
            .NotEmpty().WithMessage("RefreshToken is required.");
    }
}
