using FluentValidation;

namespace RealEstate.Application.Features.Leads.Commands.CreateLead;

public class CreateLeadCommandValidator : AbstractValidator<CreateLeadCommand>
{
    public CreateLeadCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("FullName is required")
            .MaximumLength(150).WithMessage("FullName must not exceed 150 characters.");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("A valid email is required")
            .When(x => !string.IsNullOrEmpty(x.Email));

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone is required")
            .MaximumLength(20).WithMessage("Phone must not exceed 20 characters.");

        RuleFor(x => x.UnitId)
            .GreaterThan(0).WithMessage("PropertyId must be greater than 0.");
    }
}
