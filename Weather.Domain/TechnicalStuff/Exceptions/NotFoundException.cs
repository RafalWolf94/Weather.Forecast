namespace Core.Domain.TechnicalStuff.Exceptions;

public abstract class NotFoundException : DomainException
{
    protected NotFoundException()
    {
    }

    protected NotFoundException(string message) : base(message)
    {
    }
}