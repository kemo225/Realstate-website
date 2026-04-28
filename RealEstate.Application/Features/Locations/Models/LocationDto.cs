namespace RealEstate.Application.Features.Locations.Models;

public class LocationDto
{
    public int Id { get; set; }
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string? District { get; set; }
    public string? Street { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
}
