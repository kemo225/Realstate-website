using FluentValidation;

namespace RealEstate.Application.Features.Deals.Queries.GetDeals;

public class GetDealsQueryValidator : AbstractValidator<GetDealsQuery>
{
    private static readonly string[] AllowedSortBy = ["dealDate", "createdAt", "unitName", "price"];

    public GetDealsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100);

        RuleFor(x => x.SortBy)
            .Must(sortBy => string.IsNullOrWhiteSpace(sortBy) ||
                            AllowedSortBy.Contains(sortBy, StringComparer.OrdinalIgnoreCase))
            .WithMessage("SortBy must be one of: dealDate, createdAt, unitName, price.");

        RuleFor(x => x.SortDirection)
            .Must(direction => string.IsNullOrWhiteSpace(direction) ||
                               string.Equals(direction, "asc", StringComparison.OrdinalIgnoreCase) ||
                               string.Equals(direction, "desc", StringComparison.OrdinalIgnoreCase))
            .WithMessage("SortDirection must be either asc or desc.");

    }
}
