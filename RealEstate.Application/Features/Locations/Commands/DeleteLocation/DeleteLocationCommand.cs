using MediatR;

namespace RealEstate.Application.Features.Locations.Commands.DeleteLocation;

public record DeleteLocationCommand(int Id) : IRequest<bool>;

