using RealEstate.Domain.Common;

namespace RealEstate.Domain.Entities;

public class DeveloperGallery : BaseEntity
{
    public string ImageUrl { get; set; } = string.Empty;
    public int DeveloperId { get; set; }


    public Developer? Developer { get; set; }
}
