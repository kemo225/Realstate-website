using FluentValidation;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Features.Deals.Commands.CreateDeal;

public class CreateDealCommandValidator : AbstractValidator<CreateDealCommand>
{
    public CreateDealCommandValidator()
    {
        RuleFor(x => x.UnitPlanId)
            .GreaterThan(0).WithMessage("UnitId is required.");

     


    }
}
