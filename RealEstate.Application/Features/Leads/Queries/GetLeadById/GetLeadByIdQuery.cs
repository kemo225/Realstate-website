using MediatR;
using RealEstate.Application.Features.Leads.Models;

namespace RealEstate.Application.Features.Leads.Queries.GetLeadById;

public record GetLeadByIdQuery(int Id) : IRequest<LeadDto?>;
