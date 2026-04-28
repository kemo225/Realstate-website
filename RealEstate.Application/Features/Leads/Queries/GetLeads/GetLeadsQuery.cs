using MediatR;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Features.Leads.Models;

namespace RealEstate.Application.Features.Leads.Queries.GetLeads;

public record GetLeadsQuery(
    string? SearchTerm = null,
    string? Status = null,
    int? PropertyId = null,
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<PaginatedList<LeadDto>>;
