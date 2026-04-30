using MediatR;

namespace RealEstate.Application.Features.Requests.Commands.RejectRequest;

public record RejectRequestCommand(int Id) : IRequest<bool>;
