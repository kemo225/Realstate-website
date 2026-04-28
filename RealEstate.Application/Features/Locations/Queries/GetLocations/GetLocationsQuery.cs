using MediatR;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Locations.Models;

namespace RealEstate.Application.Features.Locations.Queries.GetLocations;

public class GetLocationsQuery : IRequest<PaginatedList<LocationDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
