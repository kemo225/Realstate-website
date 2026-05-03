using RealEstate.Domain.Common;

namespace RealEstate.Domain.Entities;

/// <summary>
/// Single reusable translation table for all translatable entities.
/// Inherits BaseEntity to keep standard audit metadata.
/// </summary>
public class EntityTranslation : BaseEntity
{
    /// <summary>Which entity type this translation belongs to.</summary>
    public TranslatableEntity EntityType { get; set; }

    /// <summary>Primary key of the translated entity (Unit.Id, Project.Id, etc.).</summary>
    public int EntityId { get; set; }

    /// <summary>The field being translated: "Name" or "Description".</summary>
    public string FieldName { get; set; } = string.Empty;

    /// <summary>ISO language code: "en", "ar", "de", "pl".</summary>
    public string Language { get; set; } = string.Empty;

    /// <summary>The translated value.</summary>
    public string Value { get; set; } = string.Empty;
}

/// <summary>
/// Identifies which entity type a translation row belongs to.
/// </summary>
public enum TranslatableEntity
{
    Unit = 1,
    Project = 2,
    Service = 3,
    Facility = 4
}
