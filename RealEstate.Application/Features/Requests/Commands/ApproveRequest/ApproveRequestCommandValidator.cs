using FluentValidation;
using RealEstate.Application.Features.Projects.Commands.AddPropertyForProject;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Application.Features.Requests.Commands.ApproveRequest
{
    public class CreatePaymentplanValidator
    : AbstractValidator<ApproveRequestCommand>
    {

        public CreatePaymentplanValidator()
        {

            RuleFor(x => x.Id)
                .NotEmpty().GreaterThan(0);
            RuleForEach(x => x.PaymentPlans)
      .SetValidator(new CreatePaymentplanRequestValidator());


        }
    }
    public class CreatePaymentplanRequestValidator
  : AbstractValidator<PaymentPlanDtoCreateReqest>
    {

        public CreatePaymentplanRequestValidator()
        {

            RuleFor(x => x.PaymentType)
                .NotEmpty()
                .Must(p => p.ToLower() == "cash" || p.ToLower() == "installment")
                .WithMessage("PaymentType must be 'Cash' or 'Installment'.");

            RuleFor(x => x.CommisionRate)
               .InclusiveBetween(0, 100);
            RuleFor(x => x.PaymentType)
                .NotEmpty()
                .Must(p => p.ToLower() == "cash" || p.ToLower() == "installment")
                .WithMessage("PaymentType must be 'Cash' or 'Installment'.");

            // ? Commission & Installment logic
            When(x => x.PaymentType != null &&
    x.PaymentType.ToLower() == "installment", () =>
    {
        RuleFor(x => x.InstallmentMoths)
           .NotNull()
           .GreaterThan(0);

        RuleFor(x => x.InstallmentDownPayment)
           .InclusiveBetween(0, 100);
    });

            When(x => string.Equals(x.PaymentType, "cash", StringComparison.OrdinalIgnoreCase), () =>
            {
                RuleFor(x => x.InstallmentMoths)
                    .Equal(0);

                RuleFor(x => x.InstallmentDownPayment)
                    .Equal(0);
            });
        }
    }
}
