using MediatR;

namespace RealEstate.Application.Features.Service.Commands.DeleteSercvice;

public record DeleteSercviceCommand(int Id) : IRequest<bool>;

