using FluentValidation;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Features.Units.Commands.UpdateUnit;

public class UpdateUnitCommandValidator : AbstractValidator<UpdateUnitCommand>
{
    public UpdateUnitCommandValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("{Id} is required.");

        // English is the required base language for Name
        RuleFor(x => x.Name.En)
            .NotEmpty().WithMessage("Name in English (En) is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");



        When(x => !string.IsNullOrWhiteSpace(x.Name.De), () =>
            RuleFor(x => x.Name.De).MaximumLength(200));

        When(x => !string.IsNullOrWhiteSpace(x.Name.Pl), () =>
            RuleFor(x => x.Name.Pl).MaximumLength(200));

        // Description validation
        RuleFor(x => x.Description.En)
            .MaximumLength(2000).WithMessage("Description must not exceed 2000 characters.");

    

        When(x => !string.IsNullOrWhiteSpace(x.Description.De), () =>
            RuleFor(x => x.Description.De).MaximumLength(2000));

        When(x => !string.IsNullOrWhiteSpace(x.Description.Pl), () =>
            RuleFor(x => x.Description.Pl).MaximumLength(2000));

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

        RuleFor(p => p.IsFeatured).NotEmpty().WithMessage("{IsFeatured} is required.");

        RuleFor(p => p.NoKitchen)
            .GreaterThanOrEqualTo(0).WithMessage("{NoKitchen} must be greater than or equal to 0.");

        RuleFor(p => p.NoBathRoom)
            .GreaterThanOrEqualTo(0).WithMessage("{NoBathRoom} must be greater than or equal to 0.");
        RuleFor(p => p.NoBedRoom)
            .GreaterThanOrEqualTo(0).WithMessage("{NoBedRoom} must be greater than or equal to 0.");





    }
}

