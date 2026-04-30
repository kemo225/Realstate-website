using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using RealEstate.Domain.Common;

namespace RealEstate.Domain.Entities;

public class Deal : BaseEntity
{
    [ForeignKey("PaymentPlan")]
    public int? UnitPlanId { get; set; }
    public PaymentPlan? PaymentPlan { get; set; }
    public enDealLocation LocationDeal { get; set; }
 public string DealType { get; set; }
    public DateTime DealDate { get; set; }

    public string? ClientName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }


    
    public byte[] RowVersion { get; set; } = null!;
}
public enum enDealLocation
{
    SoldInside,
    SoldOutside
}

