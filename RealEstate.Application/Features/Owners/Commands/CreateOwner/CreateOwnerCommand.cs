using MediatR;
using RealEstate.Application.Common.Models;

namespace RealEstate.Application.Features.Owners.Commands.CreateOwner;

public record CreateOwnerCommand(
    string FullName,
    string? Email,
    string Phone,
    string? Notes
) : IRequest<Result<int>>;

