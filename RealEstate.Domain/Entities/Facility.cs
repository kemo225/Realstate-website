using System.Collections.Generic;
using RealEstate.Domain.Common;

namespace RealEstate.Domain.Entities;

public class Facility : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public ICollection<UnitFacility> PropertyFacilities { get; set; } = new List<UnitFacility>();
}
