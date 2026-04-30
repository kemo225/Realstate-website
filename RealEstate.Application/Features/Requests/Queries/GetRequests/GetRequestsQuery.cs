using MediatR;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Requests.Models;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Features.Requests.Queries.GetRequests;

public record GetRequestsQuery : IRequest<PaginatedList<RequestDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public RequestStatus? Status { get; init; }
}
