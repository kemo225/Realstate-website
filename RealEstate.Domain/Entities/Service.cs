using RealEstate.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Domain.Entities
{
    public class Service:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<UnitService> UnitServices { get; set; } = new List<UnitService>();

    }
}
