using System.Reflection;
using Core.Domain.TechnicalStuff.Exceptions;
using JetBrains.Annotations;

namespace Core.UseCases.TechnicalStuff.Cqrs;

[PublicAPI]
public class CommandHandlerInvalidTypeException(MemberInfo type, string expectedTypeName)
    : DesignErrorException($"Expected {type.GetType().Name} tobe {expectedTypeName}")
{
    public static int ErrorCode => 1142;

    public static readonly IReadOnlyDictionary<string, string> ErrorDescriptions = DescriptionForTechnicalError;

    public override int GetErrorCode() => ErrorCode;

    public override IReadOnlyDictionary<string, string> GetErrorDescriptions()
    {
        return ErrorDescriptions;
    }
}