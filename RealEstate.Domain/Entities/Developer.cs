using RealEstate.Domain.Common;
using System.Collections.Generic;

namespace RealEstate.Domain.Entities;

public class Developer : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? LogoImage { get; set; }
    public string? Description { get; set; }
    public bool IsDeleted { get; set; }

    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<DeveloperGallery> Gallery { get; set; } = new List<DeveloperGallery>();
}
