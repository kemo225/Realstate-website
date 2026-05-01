using System;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Features.Deals.Models;

public class DealDto
{
    public int Id { get; set; }
    public DateTime DealDate { get; set; }
    public string DealType { get; set; }
    public DealUnitBasicDto Unit { get; set; } = new();
    public DealUnitDetailsDto UnitDetails { get; set; } = new();
    public DealBuyerDto Buyer { get; set; } = new();
    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class DealUnitBasicDto
{
    public int UnitId { get; set; }
    public string UnitName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Area { get; set; }
    public bool IsActive { get; set; }
    public int ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
}

public class DealUnitDetailsDto
{
    public int UnitDetailId { get; set; }
    public decimal? CommissionRate { get; set; }
    public decimal? InstallmentMothes { get; set; }
    public decimal? InstallmentDownPayment { get; set; }
    public string? PaymentType { get; set; }
    public string? Status { get; set; }
}

public class DealBuyerDto
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public DateTime DealDate {  get; set; }
    public string? DealLocation { get; set; }



}

public class DealDetailsDto : DealDto
{
}
