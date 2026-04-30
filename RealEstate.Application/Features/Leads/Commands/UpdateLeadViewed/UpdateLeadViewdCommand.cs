using MediatR;
using RealEstate.Application.Common.Models;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Features.Leads.Commands.CreateLead;

public record UpdateLeadViewdCommand(int LeadId,enStatusLead status
) : IRequest<bool>;

