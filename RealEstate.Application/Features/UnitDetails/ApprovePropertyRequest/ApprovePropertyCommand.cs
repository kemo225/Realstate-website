using MediatR;
using RealEstate.Application.Common.Models;
using RealEstate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Application.Features.PropertyDetails.ApproveProperty
{
    public class ApprovePropertyCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public decimal? CommissionRate { get; set; }
        public int? InstallmentYears { get; set; }
        public decimal? InstallmentDownPayment { get; set; }
        public PaymentType? PaymentMethod { get; set; } // غيرت الاسم لتفادي الـ conflict

    }
}
