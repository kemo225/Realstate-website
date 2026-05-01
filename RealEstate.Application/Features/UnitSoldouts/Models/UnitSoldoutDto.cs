using System;
using System.Collections.Generic;

namespace RealEstate.Application.Features.UnitSoldouts.Models;

public class UnitSoldoutDto
{
    public int Id { get; set; }
    public int UnitId { get; set; }
    public string UnitName { get; set; } = string.Empty;
    public string? ProjectName { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public List<string> UnitImages { get; set; } = new();
    public DateTime SoldoutDate { get; set; }
    public string SoldType { get; set; } = string.Empty;
    public string? Notes { get; set; }

    // Audit fields
    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}
