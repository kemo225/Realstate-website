using RealEstate.Domain.Common;

namespace RealEstate.Domain.Entities;

public class Location : BaseEntity
{
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string? District { get; set; }
    public string? Street { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
}
