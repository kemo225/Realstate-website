using MediatR;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Deals.Models;

namespace RealEstate.Application.Features.Deals.Queries.GetDeals;

public record GetDealsQuery(
    int? PropertyId = null,
    string? SearchTerm = null,
    string? Status = null,
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<PaginatedList<DealDto>>;
