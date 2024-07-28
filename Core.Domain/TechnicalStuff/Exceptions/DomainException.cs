namespace Core.Domain.TechnicalStuff.Exceptions;

public abstract class DomainException : BaseException
{
    protected DomainException()
    {
    }

    protected DomainException(string message) : base(message)
    {
    }
}