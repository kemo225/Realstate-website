using FluentValidation;

namespace RealEstate.Application.Features.Locations.Commands.CreateLocation;

public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
{
    public CreateLocationCommandValidator()
    {
        RuleFor(p => p.District)
         .NotEmpty().WithMessage("District is required.")
         .MaximumLength(100);


        RuleFor(p => p.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(100);
            
    
    }
}
