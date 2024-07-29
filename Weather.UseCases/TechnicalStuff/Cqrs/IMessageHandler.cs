namespace Core.UseCases.TechnicalStuff.Cqrs;

public interface IMessageHandler
{
    Task Handle(IMessage message);
}