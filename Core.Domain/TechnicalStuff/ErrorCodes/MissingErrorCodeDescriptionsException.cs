using Core.Domain.TechnicalStuff.Exceptions;

namespace Core.Domain.TechnicalStuff.ErrorCodes;

public class MissingErrorCodeDescriptionsException(string exceptionType) : DesignErrorException($"Missing error code description{exceptionType}")
{
    public static int ErrorCode => 1174;
    public static readonly IReadOnlyDictionary<string, string> ErrorDescriptions = DescriptionForTechnicalError;

    public override int GetErrorCode() => ErrorCode;

    public override IReadOnlyDictionary<string, string> GetErrorDescriptions()
    {
        return ErrorDescriptions;
    }
}