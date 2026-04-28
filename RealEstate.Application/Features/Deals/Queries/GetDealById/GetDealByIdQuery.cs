using MediatR;
using RealEstate.Application.Features.Deals.Models;

namespace RealEstate.Application.Features.Deals.Queries.GetDealById;

public record GetDealByIdQuery(int Id) : IRequest<DealDetailsDto>;
