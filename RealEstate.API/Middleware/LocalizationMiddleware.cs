using Microsoft.AspNetCore.Http;
using RealEstate.Domain.Services;

namespace RealEstate.API.Middleware;

/// <summary>
/// Localization Middleware — runs on every request before authentication.
/// 1. Reads Accept-Language header.
/// 2. Validates against supported languages: en, ar, de, pl.
/// 3. Falls back to "en" when header is absent or language is unsupported.
/// 4. Stores resolved language in HttpContext.Items["ResolvedLanguage"].
/// </summary>
public class LocalizationMiddleware
{
    private static readonly HashSet<string> _supported =
        new(StringComparer.OrdinalIgnoreCase) { "en", "de", "pl" };

    private const string ContextKey = "ResolvedLanguage";
    private const string DefaultLanguage = "en";

    private readonly RequestDelegate _next;

    public LocalizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var language = ResolveLanguage(context);
        context.Items[ContextKey] = language;

        await _next(context);
    }

    private static string ResolveLanguage(HttpContext context)
    {
        var headerValue = context.Request.Headers["Accept-Language"].FirstOrDefault();

        if (string.IsNullOrWhiteSpace(headerValue))
            return DefaultLanguage;

        // Accept-Language can be a comma-separated list: "ar,en;q=0.9"
        // Try each entry in priority order
        var candidates = headerValue
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(entry =>
            {
                // Strip quality factor: "en;q=0.9" → "en"
                var parts = entry.Trim().Split(';');
                return parts[0].Trim().ToLowerInvariant();
            });

        foreach (var lang in candidates)
        {
            // Exact match first (e.g. "ar")
            if (_supported.Contains(lang))
                return lang;

            // Region variant match: "en-US" → "en"
            var baseLang = lang.Split('-')[0];
            if (_supported.Contains(baseLang))
                return baseLang;
        }

        return DefaultLanguage;
    }
}
