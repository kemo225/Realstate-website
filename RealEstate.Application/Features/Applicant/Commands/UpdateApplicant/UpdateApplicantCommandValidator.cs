using FluentValidation;

namespace RealEstate.Application.Features.Owners.Commands.UpdateOwner;

public class UpdateApplicantCommandValidator : AbstractValidator<UpdateApplicantCommand>
{
    public UpdateApplicantCommandValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.FullName).NotEmpty().MaximumLength(200);
        RuleFor(p => p.Phone)
            .NotEmpty()
            .Length(11)
            .Matches(@"^\+?\d+$")
            .WithMessage("Phone must contain only digits and may start with +");
        RuleFor(p => p.Email).NotEmpty().EmailAddress().When(x => !string.IsNullOrEmpty(x.Email));
    }
}

