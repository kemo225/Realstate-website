using RealEstate.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RealEstate.Domain.Entities
{
    public class UnitDetail : BaseEntity
    {
        public decimal? CommissionRate { get; set; }
        public int? InstallmentYears { get; set; }
        public decimal? InstallmentDownPayment { get; set; }
        public PaymentType? PaymentType { get; set; } 
        public PropertyStatus? Status { get; set; } 
        [ForeignKey("Unit")]
        public int UnitId { get; set; } 
        public virtual Unit? Unit { get; set; }
        public string? ApprovedById { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public virtual Deal Deal { get; set; } 


    }
    public enum PaymentType
    {
        Cash,
        Installment
    }
    public enum PropertyStatus
    {
        PendingApproval,
        Approved,
        Rejected,
        Sold,
    }
}
