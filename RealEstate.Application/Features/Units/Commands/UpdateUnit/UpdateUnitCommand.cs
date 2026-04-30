using MediatR;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Features.Units.Commands.UpdateUnit;

public class UpdateUnitCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public PropertyType PropertyType { get; set; }
    public int NoBathRoom { get; set; }
    public int NoBedRoom { get; set; }
    public int NoKitchen { get; set; }
    public string? FloorName { get; set; }
    public bool IsFeatured { get; set; }
}
//public string Name { get; set; } = string.Empty;
//public string? Description { get; set; }
//public decimal Price { get; set; }
//public int Area { get; set; }
//public int NoBathRoom { get; set; }
//public int NoBedRoom { get; set; }
//public int NoKitchen { get; set; }
//public PropertyType PropertyType { get; set; } = PropertyType.Apartment;
//public int FloorNumber { get; set; }
//public enView View { get; set; } = enView.Mountain;
//public string? FloorName { get; set; }
//public bool IsFeatured { get; set; }

//"commissionRate": 0,
//  "installmentYears": 3,
//  "installmentDownPayment": 0,
//  "paymentType": "Installment",

