using FluentValidation;

namespace RealEstate.Application.Features.Deals.Commands.CreateDeal;

public class CreateDealCommandValidator : AbstractValidator<CreateDealCommand>
{
    public CreateDealCommandValidator()
    {
   

        RuleFor(x => x.DealDate)
            .NotEmpty().WithMessage("DealDate is required.");

        RuleFor(x => x.DealType)
            .NotEmpty().WithMessage("DealType is required.")
            .Must(type =>
                string.Equals(type, "Sale", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(type, "Resale", StringComparison.OrdinalIgnoreCase))
            .WithMessage("DealType must be either Sale or Resale.");

    }
}
