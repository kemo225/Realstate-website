using MediatR;
using RealEstate.Application.Features.UnitSoldouts.Models;

namespace RealEstate.Application.Features.UnitSoldouts.Queries.GetUnitSoldoutById;

public record GetUnitSoldoutByIdQuery(int Id) : IRequest<UnitSoldoutDto>;
