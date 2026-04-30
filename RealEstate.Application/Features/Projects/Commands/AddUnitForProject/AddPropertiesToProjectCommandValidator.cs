using FluentValidation;
using RealEstate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Application.Features.Projects.Commands.AddPropertyForProject
{
    public class AddPropertiesToProjectCommandValidator
    : AbstractValidator<AddPropertiesToProjectCommand>
    {
        public AddPropertiesToProjectCommandValidator()
        {
         
            RuleFor(x => x.ProjectId)
                .GreaterThan(0);

            RuleFor(x => x.Units)
                .NotEmpty()
                .WithMessage("At least one property is required.");

            RuleForEach(x => x.Units)
                .SetValidator(new CreatePropertyDtoValidator());


            RuleFor(x => x.Units)
                .Must(properties =>
                    properties
                        .GroupBy(p => p.Name.Trim().ToLower())
                        .All(g => g.Count() == 1)
                )
                .WithMessage("Duplicate property Name are not allowed in the same request.");
        }
    }
    public class CreatePropertyDtoValidator
        : AbstractValidator<CreatePropertyDto>
    {
        public CreatePropertyDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .MaximumLength(1000);

            RuleFor(x => x.Price)
                .GreaterThan(0);
            RuleFor(x => x.PropertyType)
          .IsInEnum()
          .WithMessage(_ =>
              $"Invalid property type. Allowed values: {string.Join(", ",
                  Enum.GetValues(typeof(PropertyType))
                      .Cast<PropertyType>()
                      .Select(v => $"{v} ({(int)v})")
              )}"
          );

            RuleFor(x => x.NoBathRoom)
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.FloorNumber)
            .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Area)
            .GreaterThan(0);

            RuleFor(x => x.NoBedRoom)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.NoKithchen)
                .GreaterThanOrEqualTo(0);

            RuleForEach(x => x.PaymentPlans)
            .SetValidator(new CreatePaymentplanValidator());



            RuleFor(x => x.View)
          .IsInEnum()
          .WithMessage(_ =>
              $"Invalid view type. Allowed values: {string.Join(", ",
                  Enum.GetValues(typeof(enView))
                      .Cast<enView>()
                      .Select(v => $"{v} ({(int)v})")
              )}"
          );

        }
    }

    public class CreatePaymentplanValidator
      : AbstractValidator<PaymentPlanDtoCreate>
    {
        public CreatePaymentplanValidator()
        {

            RuleFor(x => x.PaymentType)
                .NotEmpty()
                .Must(p => p.ToLower() == "cash" || p.ToLower() == "installment")
                .WithMessage("PaymentType must be 'Cash' or 'Installment'.");

            When(x => x.PaymentType != null &&
             x.PaymentType.ToLower() == "installment", () =>
             {
                 RuleFor(x => x.InstallmentMonthes)
                    .NotNull()
                    .GreaterThan(0);

                 RuleFor(x => x.InstallmentDownPayment)
                    .InclusiveBetween(0, 100);
             });

            When(x => string.Equals(x.PaymentType, "cash", StringComparison.OrdinalIgnoreCase), () =>
            {
                RuleFor(x => x.InstallmentMonthes)
                    .Equal(0);

                RuleFor(x => x.InstallmentDownPayment)
                    .Equal(0);
            });
        }
    }
}
