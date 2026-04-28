using MediatR;

namespace RealEstate.Application.Features.Properties.Commands.DeleteProperty;

public record DeleteUnitCommand(int Id) : IRequest<bool>;

