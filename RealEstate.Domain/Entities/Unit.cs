using System.Collections.Generic;
using RealEstate.Domain.Common;

namespace RealEstate.Domain.Entities;

public class Unit : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Area { get; set; }
    public int NoBathRoom {  get; set; }
    public int NoBedRoom { get; set; }
    public int NoKitchen { get; set; }
    public PropertyType PropertyType { get; set; } = PropertyType.Apartment;
    public int FloorNumber { get; set; }
    public enView View { get; set; } = enView.Mountain;

    public enStatusUnit? Status { get; set; } 
    public enTyoeUnit ?Type { get; set; }

    public string? FloorName { get; set; }
    public bool IsFeatured { get; set; }

    public int SoldCount { get; set; } = 0;

    public int ProjectId { get; set; }
    public Project? Project { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<UnitImage> Images { get; set; } = new List<UnitImage>();
    public ICollection<UnitFacility> PropertyFacilities { get; set; } = new List<UnitFacility>();
    public ICollection<PaymentPlan> PaymentPlans { get; set; } = new List<PaymentPlan>();
    public ICollection<UnitService> UnitServices { get; set; } = new List<UnitService>();
    public ICollection<Request> Requests { get; set; } = new List<Request>();
    public ICollection<Lead> Leads { get; set; } = new List<Lead>();


    public ICollection<UnitSoldout>  UnitSoldOuts { get; set; } = new List<UnitSoldout>();


    // Concurrency token
    public byte[] RowVersion { get; set; } = null!;
}
public enum enView
{
    Sea,
    Mountain,
    Garden,
    Pool,
    SeaAndPool,
}
public enum enTyoeUnit
{
    Buy,Rent
}
public enum enStatusUnit
{
    Primary, Reslae
}
public enum PropertyType
{
    Apartment,
    Villa,
    Townhouse,
    Studio,
    Penthouse
}

public class UnitImage : BaseEntity
{
    public int PropertyId { get; set; }
    public Unit? Property { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsPrimary { get; set; }
    public int SortOrder { get; set; }
}

public class UnitFacility
{
    public int UnitId { get; set; }
    public Unit? Unit { get; set; }
    public int FacilityId { get; set; }
    public Facility? Facility { get; set; }
    public bool IsOptional { get; set; }
}
