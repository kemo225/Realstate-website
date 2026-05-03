using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Localization;

/// <summary>
/// Concrete implementation of ITranslationService.
/// All reads are batched: one DB round-trip per handler call regardless of entity count.
/// Fallback chain: requested language → English.
/// </summary>
public class TranslationService : ITranslationService
{
    private readonly IApplicationDbContext _context;

    public TranslationService(IApplicationDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<Dictionary<(int EntityId, string FieldName), string>> GetTranslationsAsync(
        TranslatableEntity entityType,
        IEnumerable<int> entityIds,
        string language,
        CancellationToken ct = default)
    {
        var idList = entityIds.ToList();

        // Single query: fetch both English (fallback) and requested language rows together.
        var rows = await _context.Translations
            .Where(t => t.EntityType == entityType
                     && idList.Contains(t.EntityId)
                     && ( t.Language == language))
            .ToListAsync(ct);

        // Build result dict. English rows go in first (lowest priority).
        // Then requested-language rows overwrite — giving natural fallback.
        var result = new Dictionary<(int, string), string>();

   
      
            foreach (var row in rows.Where(r => r.Language == language))
                result[(row.EntityId, row.FieldName)] = row.Value;
        

        return result;
    }

    /// <inheritdoc />
    public async Task SaveTranslationsAsync(
        TranslatableEntity entityType,
        int entityId,
        Dictionary<string, TranslationInputDto> fieldTranslations,
        CancellationToken ct = default)
    {
        // 1. Identify which fields we are touching
        var fieldNames = fieldTranslations.Keys.ToList();

        // 2. Remove existing translations for these fields for this specific entity
        var existing = await _context.Translations
            .Where(t => t.EntityType == entityType 
                     && t.EntityId == entityId 
                     && fieldNames.Contains(t.FieldName))
            .ToListAsync(ct);

        if (existing.Any())
        {
            _context.Translations.RemoveRange(existing);
        }

        // 3. Build new rows
        var rows = new List<EntityTranslation>();

        foreach (var (fieldName, input) in fieldTranslations)
        {
            foreach (var (lang, value) in input.ToDictionary())
            {
                if (string.IsNullOrWhiteSpace(value)) continue;

                rows.Add(new EntityTranslation
                {
                    EntityType = entityType,
                    EntityId   = entityId,
                    FieldName  = fieldName,
                    Language   = lang,
                    Value      = value
                });
            }
        }

        if (rows.Count > 0)
        {
            await _context.Translations.AddRangeAsync(rows, ct);
        }

        await _context.SaveChangesAsync(ct);
    }
}
