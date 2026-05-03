using RealEstate.Application.Common.Models;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Common.Interfaces;

/// <summary>
/// Generic translation service — reusable for all entities.
/// All read operations are batched: one DB query per handler call, zero N+1.
/// </summary>
public interface ITranslationService
{
    /// <summary>
    /// Fetches translations for a batch of entities in ONE query.
    /// Returns a dictionary keyed by (entityId, fieldName).
    /// Falls back to English if the requested language is not available.
    /// </summary>
    /// <param name="entityType">The entity type (Unit, Project, etc.)</param>
    /// <param name="entityIds">List of entity PKs to fetch translations for.</param>
    /// <param name="language">Requested language code (e.g. "ar").</param>
    /// <param name="ct">Cancellation token.</param>
    Task<Dictionary<(int EntityId, string FieldName), string>> GetTranslationsAsync(
        TranslatableEntity entityType,
        IEnumerable<int> entityIds,
        string language,
        CancellationToken ct = default);

    /// <summary>
    /// Persists translations for a single entity after it has been saved.
    /// fieldTranslations: field name → TranslationInputDto (e.g. "Name" → { En="...", Ar="..." }).
    /// </summary>
    Task SaveTranslationsAsync(
        TranslatableEntity entityType,
        int entityId,
        Dictionary<string, TranslationInputDto> fieldTranslations,
        CancellationToken ct = default);
}
