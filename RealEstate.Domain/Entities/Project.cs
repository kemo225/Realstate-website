using System.Collections.Generic;
using RealEstate.Domain.Common;

namespace RealEstate.Domain.Entities;

public class Project : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    
    public int? DeveloperId { get; set; }
    public Developer? Developer { get; set; }

    public int? LocationId { get; set; }
    public Location? Location { get; set; }

    public ICollection<ProjectImage> Images { get; set; } = new List<ProjectImage>();
    public ICollection<Unit> Properties { get; set; } = new List<Unit>();
}

public class ProjectImage : BaseEntity
{
    public int ProjectId { get; set; }
    public Project? Project { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}
