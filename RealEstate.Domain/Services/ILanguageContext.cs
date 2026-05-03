namespace RealEstate.Domain.Services;

/// <summary>
/// Scoped service that carries the resolved Accept-Language for the current HTTP request.
/// Resolved once by LocalizationMiddleware and available anywhere via DI.
/// </summary>
public interface ILanguageContext
{
    /// <summary>Resolved ISO language code (e.g. "en", "ar", "de", "pl").</summary>
    string Language { get; }

    string DefaultLanguage {get;}


    /// <summary>All languages the system supports.</summary>
    IReadOnlyList<string> SupportedLanguages { get; }
}
