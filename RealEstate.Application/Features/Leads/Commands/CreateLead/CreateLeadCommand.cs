using MediatR;
using RealEstate.Application.Common.Models;

namespace RealEstate.Application.Features.Leads.Commands.CreateLead;

public record CreateLeadCommand(
    string FullName,
    string? Email,
    string Phone,
    int UnitId,
    string? Notes
) : IRequest<Result<int>>;

