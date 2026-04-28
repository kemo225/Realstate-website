using RealEstate.Domain.Entities;
using System.Collections.Generic;

namespace RealEstate.Application.Features.Properties.Models;

public class PropertyDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string PropertyType { get; set; } = string.Empty;
    public bool IsFeatured { get; set; }
    public string LocationName { get; set; } = string.Empty;
    public string ProjectName { get; set; } = string.Empty;
    public string OwnerName { get; set; } = string.Empty;
    public decimal? CommissionRate { get; set; }
    public decimal? InstallmentYears { get; set; }
    public decimal? InstallmentDownPayment { get; set; }
    public string? PaymentType { get; set; }
    public string? Status { get; set; }
    public List<string> ImageUrls { get; set; } = new();
    public List<string> Facilities { get; set; } = new();
    public List<string> Services { get; set; } = new();


}
