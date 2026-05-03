using RealEstate.Domain.Entities;
using System;
using System.Collections.Generic;

namespace RealEstate.Application.Features.Properties.Models;

public class UnitDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    //
    public int Area { get; set; }
    public int NoBathRoom { get; set; }
    public int NoBedRoom { get; set; }
    public int NoKitchen { get; set; }
    public string? FloorName { get; set; }
    public int FloorNumber { get; set; }
    //
    public string ?UnitType { get; set; } = string.Empty;
    public string? UnitStatus { get; set; } = string.Empty;



    //
    public string PropertyType { get; set; } = string.Empty;
    public bool IsFeatured { get; set; }
    public bool IsActive { get; set; }
    public string? LocationName { get; set; }
    public string? ProjectName { get; set; }
    public List<PaymentPlanDtoRead> PaymentPlans { get; set; } = new();
    public List<string> ImageUrls { get; set; } = new();
    public List<string> Facilities { get; set; } = new();
    public List<string> Services { get; set; } = new();

    // Audit fields
    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class PaymentPlanDtoRead
{
    public string? PlanStatus { get; set; }
    public decimal? InstallmentMothes { get; set; }
    public decimal? InstallmentDownPayment { get; set; }
    public string? PaymentType { get; set; }
}
