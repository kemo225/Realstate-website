using MediatR;
using RealEstate.Application.Features.Properties.Models;

namespace RealEstate.Application.Features.Properties.Queries.GetPropertyById;

public record GetPropertyByIdQuery(int Id) : IRequest<PropertyDto?>;
