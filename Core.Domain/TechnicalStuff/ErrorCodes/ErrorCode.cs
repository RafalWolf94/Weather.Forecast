using Core.Domain.TechnicalStuff.Exceptions;
using JetBrains.Annotations;

namespace Core.Domain.TechnicalStuff.ErrorCodes;

public record ErrorCode(int Code, [UsedImplicitly] IReadOnlyDictionary<string, string> Descriptions)
{
    public static IEnumerable<ErrorCode> GetFrameworkErrorCodes() =>
    [
        DefaultExceptionErrorCode,
        ValidationExceptionErrorCode,
        DbUpdateConcurrencyExceptionErrorCode,
        BadHttpRequestExceptionErrorCode
    ];

    public static readonly ErrorCode DefaultExceptionErrorCode = new(-1, BaseException.DescriptionForTechnicalError);

    public static readonly ErrorCode ValidationExceptionErrorCode = new(1001, new Dictionary<string, string>()
    {
        { "pl", "Zapytanie zawiera błędne dane, które nie moą byc poprawnie zwalidowane" },
        { "en", "Request contains invalid data that cannot be correctly validated" }
    });

    public static readonly ErrorCode DbUpdateConcurrencyExceptionErrorCode = new(1002, new Dictionary<string, string>()
    {
        { "pl", "Dane zmieniły sie od ostatniej aktualizacji - należy ponowić próbę" },
        { "en", "Data has changed since last update - try again" }
    });

    public static readonly ErrorCode BadHttpRequestExceptionErrorCode = new(1003, new Dictionary<string, string>()
    {
        { "pl", "Zapytanie zawiera błędne dane, które nie moą byc poprawnie zwalidowane" },
        { "en", "Request contains invalid data that cannot be correctly validated" }
    });
}