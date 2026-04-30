using RealEstate.Domain.Common;

namespace RealEstate.Domain.Entities;

public class Location : BaseEntity
{
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string? District { get; set; }
    public string? Street { get; set; }
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
}
