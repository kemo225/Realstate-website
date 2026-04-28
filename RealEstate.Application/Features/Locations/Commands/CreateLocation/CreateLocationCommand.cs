using MediatR;

namespace RealEstate.Application.Features.Locations.Commands.CreateLocation;

public class CreateLocationCommand : IRequest<int>
{
    public string City { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
}
