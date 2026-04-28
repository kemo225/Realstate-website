using MediatR;
using RealEstate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Application.Features.Units.Commands.UpdateUnit
{
    public class UnitSelectDto : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public PropertyType PropertyType { get; set; }
        public string? PaymentType { get; set; }
        public decimal? CmmisionRate { get; set; }
        public decimal? installmentYears { get; set; }
        public decimal? installmentDownPayment { get; set; }
        public int ProjectId { get; set; }



    }
}
