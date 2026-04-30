using MediatR;
using RealEstate.Application.Features.Properties.Models;

namespace RealEstate.Application.Features.Properties.Queries.GetPropertyById;

public record GetUnitByIdQuery(int Id) : IRequest<UnitDto?>;
