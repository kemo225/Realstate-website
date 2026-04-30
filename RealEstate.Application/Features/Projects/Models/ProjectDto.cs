using System.Collections.Generic;

namespace RealEstate.Application.Features.Projects.Models;

public class ProjectDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? DeveloperId { get; set; }
    public string? DeveloperName { get; set; }
    public int? LocationId { get; set; }
    public string? LocationName { get; set; }
    public List<string> ImageUrls { get; set; } = new();
    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
