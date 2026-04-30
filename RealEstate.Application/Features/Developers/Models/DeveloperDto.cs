using System;
using System.Collections.Generic;

namespace RealEstate.Application.Features.Developers.Models;

public class DeveloperDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? LogoImage { get; set; }
    public string? Description { get; set; }
    
    public List<DeveloperGalleryDto> Gallery { get; set; } = new();
    public List<DeveloperProjectDto> Projects { get; set; } = new();

    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class DeveloperGalleryDto
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}

public class DeveloperProjectDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
