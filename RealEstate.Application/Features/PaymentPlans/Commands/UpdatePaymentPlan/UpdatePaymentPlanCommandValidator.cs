using FluentValidation;

namespace RealEstate.Application.Features.PaymentPlans.Commands.UpdatePaymentPlan;

public class UpdatePaymentPlanCommandValidator : AbstractValidator<UpdatePaymentPlanCommand>
{
    public UpdatePaymentPlanCommandValidator()
    {

        RuleFor(x => x.paymentPlanId)
         .NotEmpty().WithMessage("paymentPlanId is required.");

        RuleFor(x => x.PaymentType)
                   .NotEmpty()
                   .Must(p => p.ToLower() == "cash" || p.ToLower() == "installment")
                   .WithMessage("PaymentType must be 'Cash' or 'Installment'.");

        // ? Commission & Installment logic
        When(x => x.PaymentType.ToLower() == "installment", () =>
        {
            RuleFor(x => x.InstallmentYears)
                .NotNull()
                .GreaterThan(0);

            RuleFor(x => x.InstallmentDownPayment)
                .NotNull()
                .GreaterThanOrEqualTo(0);
        });

        When(x => x.PaymentType.ToLower() == "cash", () =>
        {
            RuleFor(x => x.InstallmentYears)
                .Equal(0);

            RuleFor(x => x.InstallmentDownPayment)
                .Equal(0);
        });
    }
}
