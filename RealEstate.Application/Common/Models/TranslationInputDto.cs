namespace RealEstate.Application.Common.Models;

/// <summary>
/// Reusable DTO for providing translations in Create/Update commands.
/// English is required (it is also stored on the entity itself as the default).
/// Other languages are optional — only non-null values are persisted.
/// </summary>
public class TranslationInputDto
{
    /// <summary>English value (required — used as the entity's base field).</summary>
    public string En { get; set; } = string.Empty;


    /// <summary>German translation (optional).</summary>
    public string? De { get; set; }

    /// <summary>Polish translation (optional).</summary>
    public string? Pl { get; set; }

    /// <summary>Converts the DTO to a language → value dictionary, skipping nulls.</summary>
    public Dictionary<string, string> ToDictionary()
    {
        var dict = new Dictionary<string, string>
        {
            ["en"] = En
        };
        if (!string.IsNullOrWhiteSpace(De)) dict["de"] = De!;
        if (!string.IsNullOrWhiteSpace(Pl)) dict["pl"] = Pl!;
        return dict;
    }
}
