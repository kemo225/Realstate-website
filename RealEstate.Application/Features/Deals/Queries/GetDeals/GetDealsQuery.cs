using MediatR;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Deals.Models;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Features.Deals.Queries.GetDeals;

public record GetDealsQuery(
    int? UnitId = null,
    int? ProjectId = null,
    enDealLocation? DealLocation = null,
    string? SortBy = "dealDate",
    string? SortDirection = "desc",
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<PaginatedList<DealDto>>;
