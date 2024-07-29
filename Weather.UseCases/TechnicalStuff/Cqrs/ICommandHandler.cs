namespace Core.UseCases.TechnicalStuff.Cqrs;

public interface ICommandHandler<in TCommand> : IMessageHandler where TCommand : ICommand
{
    Task IMessageHandler.Handle(IMessage message)
    {
        if (message is not TCommand command)
            throw new CommandHandlerInvalidTypeException(message.GetType(), GetType().Name);

        return Handle(command);
    }

    Task Handle(TCommand command);
}

public interface ICommandHandler<in TCommand, TResult> where TCommand : ICommand
{
    Task<TResult> Handle(TCommand command);
}