using FluentValidation;

namespace RealEstate.Application.Features.Developers.Commands.CreateDeveloper;

public class CreateDeveloperCommandValidator : AbstractValidator<CreateDeveloperCommand>
{
    public CreateDeveloperCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");
            
        RuleFor(v => v.Description)
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");
    }
}
