using MediatR;

namespace RealEstate.Application.Features.Locations.Commands.CreateLocation;

public class CreateLocationCommand : IRequest<int>
{
    public string City { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
}
