using FluentValidation;

namespace RealEstate.Application.Features.Projects.Commands.CreateProject;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(150);

        RuleFor(p => p.LocationId)
            .NotEmpty().WithMessage("LocationId is required.");
    }
}
