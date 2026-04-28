using FluentValidation;

namespace RealEstate.Application.Features.Locations.Commands.UpdateLocation;

public class UpdateLocationCommandValidator : AbstractValidator<UpdateLocationCommand>
{
    public UpdateLocationCommandValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(100);

        RuleFor(p => p.District)
            .NotEmpty().WithMessage("District is required.")
            .MaximumLength(100);

        RuleFor(p => p.Street)
            .NotEmpty().WithMessage("Street is required.")
            .MaximumLength(100);

        RuleFor(p => p.Country)
            .NotEmpty().WithMessage("Country is required.")
            .MaximumLength(100);
    }
}
