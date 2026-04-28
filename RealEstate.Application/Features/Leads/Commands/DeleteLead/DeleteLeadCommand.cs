using MediatR;
using RealEstate.Application.Common.Models;

namespace RealEstate.Application.Features.Leads.Commands.DeleteLead;

public record DeleteLeadCommand(int Id) : IRequest<Result>;
