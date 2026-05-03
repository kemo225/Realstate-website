using FluentValidation;

namespace RealEstate.Application.Features.Service.Commands.CreateSercvice;

public class CreateSercviceCommandValidator : AbstractValidator<CreateSercviceCommand>
{
    public CreateSercviceCommandValidator()
    {
        RuleFor(x => x.Name.En)
            .NotEmpty().WithMessage("Name in English (En) is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

   

        When(x => !string.IsNullOrWhiteSpace(x.Name.De), () =>
            RuleFor(x => x.Name.De).MaximumLength(100));

        When(x => !string.IsNullOrWhiteSpace(x.Name.Pl), () =>
            RuleFor(x => x.Name.Pl).MaximumLength(100));
    }
}
