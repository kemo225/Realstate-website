using FluentValidation;

namespace RealEstate.Application.Features.Projects.Commands.CreateProject;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        // English is the required base language for Name
        RuleFor(x => x.Name.En)
            .NotEmpty().WithMessage("Name in English (En) is required.")
            .MaximumLength(150).WithMessage("Name must not exceed 150 characters.");

        When(x => !string.IsNullOrWhiteSpace(x.Name.De), () =>
            RuleFor(x => x.Name.De).MaximumLength(150));

        When(x => !string.IsNullOrWhiteSpace(x.Name.Pl), () =>
            RuleFor(x => x.Name.Pl).MaximumLength(150));

        // Description is optional but has length limits
        RuleFor(x => x.Description.En)
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

        When(x => !string.IsNullOrWhiteSpace(x.Description.De), () =>
            RuleFor(x => x.Description.De).MaximumLength(1000));

        When(x => !string.IsNullOrWhiteSpace(x.Description.Pl), () =>
            RuleFor(x => x.Description.Pl).MaximumLength(1000));

        RuleFor(p => p.LocationId)
            .NotEmpty().WithMessage("LocationId is required.");
        
        RuleFor(p => p.DeveloperId)
            .NotEmpty().WithMessage("DeveloperId is required.");
    }
}
