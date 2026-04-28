using MediatR;

namespace RealEstate.Application.Features.Facilities.Commands.DeleteFacility;

public record DeleteSercviceCommand(int Id) : IRequest<bool>;

