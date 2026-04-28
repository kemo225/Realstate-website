using System.Collections.Generic;
using RealEstate.Domain.Common;

namespace RealEstate.Domain.Entities;

public class Owner : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string? Notes { get; set; }

    public ICollection<Unit> Properties { get; set; } = new List<Unit>();
    public ICollection<Deal> BoughtDeals { get; set; } = new List<Deal>();
    public ICollection<Deal> SoldDeals { get; set; } = new List<Deal>();
}
