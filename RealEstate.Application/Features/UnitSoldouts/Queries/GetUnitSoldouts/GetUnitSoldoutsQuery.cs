using MediatR;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.UnitSoldouts.Models;

namespace RealEstate.Application.Features.UnitSoldouts.Queries.GetUnitSoldouts;

public record GetUnitSoldoutsQuery(
    string? UnitName = null,
    string? SoldType = null,
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<PaginatedList<UnitSoldoutDto>>;
