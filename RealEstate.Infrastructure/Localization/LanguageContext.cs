using Microsoft.AspNetCore.Http;
using RealEstate.Domain.Services;

namespace RealEstate.Infrastructure.Localization;

/// <summary>
/// Scoped implementation of ILanguageContext.
/// Reads the resolved language from HttpContext.Items (set by LocalizationMiddleware).
/// Defaults to "en" if not set.
/// </summary>
public class LanguageContext : ILanguageContext
{
    public string DefaultLanguage {  get; }="en";

    private const string HttpContextKey = "ResolvedLanguage";

    private static readonly IReadOnlyList<string> _supported =
        new[] { "en",  "de", "pl" };

    private readonly IHttpContextAccessor _httpContextAccessor;

    public LanguageContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public IReadOnlyList<string> SupportedLanguages => _supported;

    public string Language
    {
        get
        {
            var ctx = _httpContextAccessor.HttpContext;
            if (ctx is null) return "en";

            return ctx.Items.TryGetValue(HttpContextKey, out var lang) && lang is string s
                ? s
                : DefaultLanguage;
        }
    }

}
