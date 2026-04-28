using RealEstate.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RealEstate.Domain.Entities
{
    public class UnitService: BaseEntity
    {
        [ForeignKey("Unit")]

        public int UnitId {  get; set; }
        
        public Unit Unit { get; set; }
        [ForeignKey("Service")]
        public int ServiceId { get; set; }

        public Service Service { get; set; }

    }
}
