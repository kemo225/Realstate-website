using FluentValidation;

namespace RealEstate.Application.Features.PaymentPlans.Commands.CreatePaymentPlan;

public class CreatePaymentPlanCommandValidator : AbstractValidator<CreatePaymentPlanCommand>
{
    public CreatePaymentPlanCommandValidator()
    {
        RuleFor(x => x.UnitId)
            .NotEmpty().WithMessage("UnitId is required.");

        RuleFor(x => x.PaymentType)
                   .NotEmpty()
                   .Must(p => p.ToLower() == "cash" || p.ToLower() == "installment")
                   .WithMessage("PaymentType must be 'Cash' or 'Installment'.");

        // ? Commission & Installment logic
        When(x => x.PaymentType != null &&
           x.PaymentType.ToLower() == "installment", () =>
           {
               RuleFor(x => x.InstallmentYears)
                  .NotNull()
                  .GreaterThan(0);

               RuleFor(x => x.InstallmentDownPayment)
                  .InclusiveBetween(0, 100);
           });

        When(x => string.Equals(x.PaymentType, "cash", StringComparison.OrdinalIgnoreCase), () =>
        {
            RuleFor(x => x.InstallmentYears)
                .Equal(0);

            RuleFor(x => x.InstallmentDownPayment)
                .Equal(0);
        });
    }
}
