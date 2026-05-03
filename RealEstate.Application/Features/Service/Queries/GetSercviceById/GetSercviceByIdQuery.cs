using MediatR;
using RealEstate.Application.Features.Facilities.Models;

namespace RealEstate.Application.Features.Service.Queries.GetSercviceById;

public record GetSercviceByIdQuery(int Id) : IRequest<SercviceDto?>;
