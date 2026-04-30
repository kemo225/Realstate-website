using System;
using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Common;

public interface IAuditableEntity
{
    DateTime CreatedAt { get; set; }
    string? CreatedById { get; set; }
    ApplicationUser? CreatedByUser { get; set; }
    DateTime? UpdatedAt { get; set; }
    string? UpdatedById { get; set; }
    ApplicationUser? UpdatedByUser { get; set; }
}
