using MediatR;
using RealEstate.Application.Features.Facilities.Models;

namespace RealEstate.Application.Features.Facilities.Queries.GetFacilities;

public record GetFacilitiesQuery : IRequest<IEnumerable<FacilityDto>>;
