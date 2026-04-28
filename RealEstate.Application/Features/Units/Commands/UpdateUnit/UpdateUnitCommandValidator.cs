using FluentValidation;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Features.Properties.Commands.UpdateProperty;

public class UpdateUnitCommandValidator : AbstractValidator<UpdateUnitCommand>
{
    public UpdateUnitCommandValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("{Id} is required.");

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{Name} is required.")
            .NotNull()
            .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters.");

        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("{Price} must be greater than 0.");

        RuleFor(x => x.PropertyType)
           .IsInEnum()
           .WithMessage(_ =>
               $"Invalid property type. Allowed values: {string.Join(", ",
                   Enum.GetValues(typeof(PropertyType))
                       .Cast<PropertyType>()
                       .Select(v => $"{v} ({(int)v})")
               )}"
           );

        RuleFor(x => x.PaymentType)
            .NotEmpty()
            .Must(p => p.ToLower() == "cash" || p.ToLower() == "installment")
            .WithMessage("PaymentType must be 'Cash' or 'Installment'.");

        When(x => x.PaymentType.ToLower() == "installment", () =>
        {
            RuleFor(x => x.installmentYears)
                .NotNull()
                .GreaterThan(0);

            RuleFor(x => x.installmentDownPayment)
                .NotNull()
                .GreaterThanOrEqualTo(0);
        });

        When(x => x.PaymentType.ToLower() == "cash", () =>
        {
            RuleFor(x => x.installmentYears)
                .Equal(0);

            RuleFor(x => x.installmentDownPayment)
                .Equal(0);
        });
    }
}

