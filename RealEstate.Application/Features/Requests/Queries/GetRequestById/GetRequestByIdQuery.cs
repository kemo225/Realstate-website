using MediatR;
using RealEstate.Application.Features.Requests.Models;

namespace RealEstate.Application.Features.Requests.Queries.GetRequestById;

public record GetRequestByIdQuery(int Id) : IRequest<RequestDetailsDto>;
