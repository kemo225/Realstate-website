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

            RuleFor(x => x.Properties)
                .NotEmpty()
                .WithMessage("At least one property is required.");

            // Validate each property باستخدام Validator تاني
            RuleForEach(x => x.Properties)
                .SetValidator(new CreatePropertyDtoValidator());

            // ❗ منع تكرار Title داخل نفس الـ Request
            RuleFor(x => x.Properties)
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

            RuleFor(x => x.PaymentType)
                .NotEmpty()
                .Must(p => p.ToLower() == "cash" || p.ToLower() == "installment")
                .WithMessage("PaymentType must be 'Cash' or 'Installment'.");

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

   

            // ✅ Commission & Installment logic
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
}
