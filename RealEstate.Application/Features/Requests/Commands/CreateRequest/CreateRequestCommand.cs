using MediatR;

namespace RealEstate.Application.Features.Requests.Commands.CreateRequest;

public record CreateRequestCommand : IRequest<int>
{
    public string UnitName { get; init; } = string.Empty;
    public int ApplicantId { get; init; }
}
