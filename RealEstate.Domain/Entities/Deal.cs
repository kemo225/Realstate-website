using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using RealEstate.Domain.Common;

namespace RealEstate.Domain.Entities;

public class Deal : BaseEntity
{
    [ForeignKey("UnitRequest")]
    public int UnitRequestId { get; set; }
    public UnitDetail? UnitRequest { get; set; }

 
    public DateTime DealDate { get; set; }

    public string? ClientName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }


    
    public byte[] RowVersion { get; set; } = null!;
}


