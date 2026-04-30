using System;
using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Common;

public abstract class BaseEntity : IAuditableEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedById { get; set; }
    public ApplicationUser? CreatedByUser { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedById { get; set; }
    public ApplicationUser? UpdatedByUser { get; set; }
}
