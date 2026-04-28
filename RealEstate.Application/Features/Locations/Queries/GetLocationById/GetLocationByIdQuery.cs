using MediatR;
using RealEstate.Application.Features.Locations.Models;

namespace RealEstate.Application.Features.Locations.Queries.GetLocationById;

public record GetLocationByIdQuery(int Id) : IRequest<LocationDto?>;
