using MediatR;

namespace RealEstate.Application.Features.Locations.Commands.UpdateLocation;

public class UpdateLocationCommand : IRequest<int>
{
    public int Id { get; set; }
    public string City { get; set; } = string.Empty;
    public string? District { get; set; }
    public string? Street { get; set; }
    public string? Country { get; set; }
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
}
