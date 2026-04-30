using MediatR;
using RealEstate.Application.Common.Models;

namespace RealEstate.Application.Features.Owners.Commands.DeleteOwner;

public record DeleteApplicantCommand(int Id) : IRequest<Result<bool>>;

