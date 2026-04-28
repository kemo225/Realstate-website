using System.Collections.Generic;
using RealEstate.Domain.Common;

namespace RealEstate.Domain.Entities;

public class Lead : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string Phone { get; set; } = string.Empty;
    public int PropertyId { get; set; }
    public Unit? Property { get; set; }

    //// New, Contacted, Qualified, Converted, Lost
    public enStatusLead StatusLead { get; set; }
    public string? Notes { get; set; }
    public bool? isActive { get; set; }=true;


    public ICollection<Deal> Deals { get; set; } = new List<Deal>();
}


public enum enStatusLead
{
    Pending,
    Followed
}
