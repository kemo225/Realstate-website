using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using RealEstate.Domain.Common;

namespace RealEstate.Domain.Entities;

public class Lead : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string Phone { get; set; } = string.Empty;
    [ForeignKey("Unit")]
    public int UnitId { get; set; }
    public Unit? Unit { get; set; }

    //// New, Contacted, Qualified, Converted, Lost
    public enStatusLead StatusLead { get; set; }
    public string? Notes { get; set; }


    public ICollection<Deal> Deals { get; set; } = new List<Deal>();
}


public enum enStatusLead
{
    Pending,
    Viewed,
    cancelled
}
