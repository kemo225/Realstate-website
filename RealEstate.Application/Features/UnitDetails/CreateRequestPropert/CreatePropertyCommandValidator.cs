using FluentValidation;

namespace RealEstate.Application.Features.Properties.Commands.CreateProperty;

public class CreatePropertyCommandValidator : AbstractValidator<CreateRequestPropertPayCommand>
{
    public CreatePropertyCommandValidator()
    {
        RuleFor(p => p.Request.Name)
            .NotEmpty().WithMessage("{Title} is required.")
            .NotNull()
            .MaximumLength(200).WithMessage("{Title} must not exceed 200 characters.");

        RuleFor(p => p.Request.Price)
            .GreaterThan(0).WithMessage("{Price} must be greater than 0.");


        RuleFor(p => p.Request.ProjectId)
            .NotEmpty().WithMessage("{ProjectId} is required.");

        RuleFor(p => p.Request.OwnerId)
            .GreaterThan(0)
            .WithMessage("{OwnerId} must be greater than 0.");
    }
}
