using MediatR;
using RealEstate.Application.Features.Owners.Queries.GetOwners;

namespace RealEstate.Application.Features.Owners.Queries.GetOwnerById;

public record GetOwnerByIdQuery(int Id) : IRequest<OwnerDto?>;
