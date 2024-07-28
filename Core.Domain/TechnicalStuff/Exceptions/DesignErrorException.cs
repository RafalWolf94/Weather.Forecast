namespace Core.Domain.TechnicalStuff.Exceptions;

public abstract class DesignErrorException : BaseException
{
    protected DesignErrorException()
    {
    }

    protected DesignErrorException(string message) : base(message)
    {
    }
}