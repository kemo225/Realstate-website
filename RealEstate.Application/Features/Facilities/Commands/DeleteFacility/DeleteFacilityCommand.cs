using MediatR;

namespace RealEstate.Application.Features.Facilities.Commands.DeleteFacility;

public record DeleteFacilityCommand(int Id) : IRequest<bool>;

