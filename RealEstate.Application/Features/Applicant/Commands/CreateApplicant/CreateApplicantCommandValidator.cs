using FluentValidation;

namespace RealEstate.Application.Features.Owners.Commands.CreateOwner;

public class CreateApplicantCommandValidator : AbstractValidator<CreateApplicantCommand>
{
    public CreateApplicantCommandValidator()
    {
        RuleFor(p => p.FullName).NotEmpty().MaximumLength(200);
        RuleFor(p => p.Phone)
            .NotEmpty()
            .Length(11)
            .Matches(@"^\+?\d+$")
            .WithMessage("Phone must contain only digits and may start with +");
        RuleFor(p => p.Email).NotEmpty().EmailAddress().When(x => !string.IsNullOrEmpty(x.Email));
    }
}

