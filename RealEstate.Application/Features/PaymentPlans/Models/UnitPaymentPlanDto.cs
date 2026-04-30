using RealEstate.Domain.Entities;
using System;

namespace RealEstate.Application.Features.PaymentPlans.Models;

public class UnitPaymentPlanDto
{
    public int Id { get; set; }
    public int UnitId { get; set; }
    public string UnitName { get; set; } = string.Empty;
    public decimal InstallmentDownPayment { get; set; }
    public int InstallmentYears { get; set; }
    public string PaymentType { get; set; }
    public string UnitStatus { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
