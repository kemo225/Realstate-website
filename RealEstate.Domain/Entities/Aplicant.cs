using System.Collections.Generic;
using RealEstate.Domain.Common;

namespace RealEstate.Domain.Entities;

public class Aplicant : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string? Notes { get; set; }

    public ICollection<Request> Requests { get; set; } = new List<Request>();

}
