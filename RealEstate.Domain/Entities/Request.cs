using RealEstate.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Domain.Entities
{
    public class Request : BaseEntity
    {
        public int UnitId { get; set; }
        [ForeignKey("UnitId")]
        public virtual Unit Unit { get; set; }

        public int ApplicantId { get; set; }
        [ForeignKey("ApplicantId")]
        public virtual Aplicant Applicant { get; set; }

        public RequestStatus Status { get; set; } = RequestStatus.Pending;

        public string? ApprovedById { get; set; }
        [ForeignKey("ApprovedById")]
        public virtual ApplicationUser? ApprovedByUser { get; set; }
        public DateTime? ApprovedAt { get; set; }
    }

    public enum RequestStatus
    {
        Pending,
        Approved,
        Rejected
    }
}
