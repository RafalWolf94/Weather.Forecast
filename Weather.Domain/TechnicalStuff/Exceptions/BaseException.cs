namespace Core.Domain.TechnicalStuff.Exceptions;

public abstract class BaseException : Exception
{
    public static readonly IReadOnlyDictionary<string, string> DescriptionForTechnicalError = new Dictionary<string, string>()
    {
        { "pl", "Błąd techniczny po stronie Serwera" },
        { "en", "Błąd techniczny po stronie Serwera" }
    };

    protected BaseException()
    {
    }

    protected BaseException(string message) : base(message)
    {
    }

    public abstract int GetErrorCode();
    public abstract IReadOnlyDictionary<string, string> GetErrorDescriptions();
}