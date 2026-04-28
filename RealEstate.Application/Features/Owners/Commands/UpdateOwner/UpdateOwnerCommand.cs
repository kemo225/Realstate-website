using MediatR;
using RealEstate.Application.Common.Models;

namespace RealEstate.Application.Features.Owners.Commands.UpdateOwner;

public class UpdateOwnerCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string? Notes { get; set; }
}

