using FluentValidation;

namespace RealEstate.Application.Features.Facilities.Commands.CreateFacility;

public class CreateFacilityCommandValidator : AbstractValidator<CreateFacilityCommand>
{
    public CreateFacilityCommandValidator()
    {
        // English is the required base language
        RuleFor(x => x.Name.En)
            .NotEmpty().WithMessage("Name in English (En) is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");


        When(x => !string.IsNullOrWhiteSpace(x.Name.De), () =>
            RuleFor(x => x.Name.De).MaximumLength(200));

        When(x => !string.IsNullOrWhiteSpace(x.Name.Pl), () =>
            RuleFor(x => x.Name.Pl).MaximumLength(200));
    }
}

