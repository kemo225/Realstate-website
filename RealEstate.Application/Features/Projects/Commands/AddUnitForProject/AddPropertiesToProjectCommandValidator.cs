using FluentValidation;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Features.Projects.Commands.AddPropertyForProject
{
    public class AddPropertiesToProjectCommandValidator : AbstractValidator<AddPropertiesToProjectCommand>
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
                        .GroupBy(p => (p.Name?.En ?? string.Empty).Trim().ToLowerInvariant())
                        .All(g => g.Count() == 1))
                .WithMessage("Duplicate property names are not allowed in the same request.");
        }
    }

    public class CreatePropertyDtoValidator : AbstractValidator<CreatePropertyDto>
    {
        public CreatePropertyDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name translations are required.");

            RuleFor(x => x.Description)
                .NotNull().WithMessage("Description translations are required.");

            RuleFor(x => x.Name.En)
                .NotEmpty()
                .MaximumLength(200);

    

            When(x => !string.IsNullOrWhiteSpace(x.Name.De), () =>
                RuleFor(x => x.Name.De).MaximumLength(200));

            When(x => !string.IsNullOrWhiteSpace(x.Name.Pl), () =>
                RuleFor(x => x.Name.Pl).MaximumLength(200));

            RuleFor(x => x.Description.En)
                .MaximumLength(1000);

            When(x => !string.IsNullOrWhiteSpace(x.Description.De), () =>
                RuleFor(x => x.Description.De).MaximumLength(1000));

            When(x => !string.IsNullOrWhiteSpace(x.Description.Pl), () =>
                RuleFor(x => x.Description.Pl).MaximumLength(1000));

            RuleFor(x => x.Price)
                .GreaterThan(0);

            RuleFor(x => x.PropertyType)
                .IsInEnum()
                .WithMessage(_ =>
                    $"Invalid property type. Allowed values: {string.Join(", ",
                        Enum.GetValues(typeof(PropertyType))
                            .Cast<PropertyType>()
                            .Select(v => $"{v} ({(int)v})"))}");

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

            RuleFor(x => x.Type)
                .NotEmpty()
                .Must(t =>
                    string.Equals(t, enTyoeUnit.Buy.ToString(), StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(t, enTyoeUnit.Rent.ToString(), StringComparison.OrdinalIgnoreCase))
                .WithMessage("Type must be Buy or Rent.");

            When(x => string.Equals(x.Type, enTyoeUnit.Buy.ToString(), StringComparison.OrdinalIgnoreCase), () =>
            {
                RuleFor(x => x.Status)
                    .NotEmpty()
                    .Must(status => Enum.TryParse<enStatusUnit>(status, true, out _))
                    .WithMessage(_ =>
                        $"Invalid status type. Allowed values: {string.Join(", ",
                            Enum.GetValues(typeof(enStatusUnit))
                                .Cast<enStatusUnit>()
                                .Select(v => $"{v} ({(int)v})"))}");
            });

            When(x => string.Equals(x.Type, enTyoeUnit.Rent.ToString(), StringComparison.OrdinalIgnoreCase), () =>
            {
                RuleFor(x => x.Status)
                    .Empty()
                    .WithMessage("Status must be empty when Type is Rent.");
            });

            RuleFor(x => x.View)
                .IsInEnum()
                .WithMessage(_ =>
                    $"Invalid view type. Allowed values: {string.Join(", ",
                        Enum.GetValues(typeof(enView))
                            .Cast<enView>()
                            .Select(v => $"{v} ({(int)v})"))}");
        }
    }

    public class CreatePaymentplanValidator : AbstractValidator<PaymentPlanDtoCreate>
    {
        public CreatePaymentplanValidator()
        {
            RuleFor(x => x.PaymentType)
                .NotEmpty()
                .Must(p =>
                    string.Equals(p, "cash", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(p, "installment", StringComparison.OrdinalIgnoreCase))
                .WithMessage("PaymentType must be 'Cash' or 'Installment'.");

            When(x => string.Equals(x.PaymentType, "installment", StringComparison.OrdinalIgnoreCase), () =>
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
