using MediatR;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Features.Properties.Commands.UpdateProperty;

public class UpdateUnitCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public PropertyType PropertyType { get; set; }
    public string? PaymentType { get; set; }
    public decimal? CmmisionRate { get; set; }
    public decimal? installmentYears { get; set; }
    public decimal ?installmentDownPayment { get; set; }


}

//"commissionRate": 0,
//  "installmentYears": 3,
//  "installmentDownPayment": 0,
//  "paymentType": "Installment",

