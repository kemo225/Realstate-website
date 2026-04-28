using MediatR;
using RealEstate.Application.Features.Facilities.Models;

namespace RealEstate.Application.Features.Facilities.Queries.GetFacilityById;

public record GetSercviceByIdQuery(int Id) : IRequest<SercviceDto?>;
