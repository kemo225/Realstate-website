using RealEstate.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RealEstate.Domain.Entities
{
    public class UnitSoldout:BaseEntity
    {
        [ForeignKey("Unit")]
        public int UnitId { get; set; }
        public DateTime SoldoutDate { get; set; } = DateTime.Now;
        public virtual Unit Unit { get; set; }= null!;
        public string SoldType { get; set; }= string.Empty;

        public string Notes {  get; set; }=string.Empty;
    }
}
