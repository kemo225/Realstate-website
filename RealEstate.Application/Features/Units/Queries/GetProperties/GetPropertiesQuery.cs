using System.Collections.Generic;
using MediatR;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Properties.Models;

namespace RealEstate.Application.Features.Properties.Queries.GetProperties;

public record GetPropertiesQuery(
    string? SearchTerm = null,
    string? PropertyType = null,
    string? Status = null,
    decimal? MinPrice = null,
    decimal? MaxPrice = null,
    int? LocationId = null,
    int? ProjectId = null,
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<PaginatedList<PropertyDto>>;
