using FluentValidation;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Features.Deals.Commands.CreateDeal;

public class CreateDealCommandValidator : AbstractValidator<CreateDealCommand>
{
    public CreateDealCommandValidator()
    {
        RuleFor(x => x.UnitPlanId)
            .GreaterThan(0).WithMessage("UnitId is required.");

     

        RuleFor(x => x.DealLocation)
            .NotEmpty()
            .Must(location => location.ToLower() == "soldinside" || location.ToLower() == "soldoutside")
            .WithMessage("DealLocation must be either SoldInside or SoldOutside.");

        When(x => x.DealLocation.ToLower() == "soldoutside", () =>
        {
            RuleFor(x => x.FullName)
                .Equal("");
            RuleFor(x => x.Email)
                         .Equal("");
            RuleFor(x => x.Phone)
                         .Equal("");

        });
        When(x => x.DealLocation.ToLower() == "soldinside", () =>
        {
            RuleFor(x => x.FullName)
             .NotEmpty().WithMessage("FullName is required.")
             .MaximumLength(150);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone is required.")
                .MaximumLength(25);

        });

    }
}
