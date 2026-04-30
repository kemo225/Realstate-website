using MediatR;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Auth.Models;

namespace RealEstate.Application.Features.Auth.Queries.GetAdmins;

public record GetAdminsQuery(
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<IEnumerable<AdminListItemDto>>;
