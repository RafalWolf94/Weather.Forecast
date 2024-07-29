using Core.UseCases.TechnicalStuff.Cqrs;
using Microsoft.Extensions.Logging;

namespace Core.UseCases.TechnicalStuff.Transactions;

public class TransactionCommandHandlerDecorator<TCommand>(
    ICommandHandler<TCommand> handler,
    ITransactionContext transactionContext,
    ILogger<TransactionCommandHandlerDecorator<TCommand>> logger)
    : ICommandHandler<TCommand>
    where TCommand : ICommand
{
    public async Task Handle(TCommand command)
    {
        using var ts = transactionContext.CreateTransactionScope();
        logger.LogInformation($"Method {command.ToString() ?? string.Empty} is used");
        await handler.Handle(command);
        await transactionContext.SaveAsync();
        ts.Complete();
    }
}

public class TransactionCommandHandlerDecorator<TCommand, TResult>(
    ICommandHandler<TCommand, TResult> handler,
    ITransactionContext transactionContext,
    ILogger<TransactionCommandHandlerDecorator<TCommand, TResult>> logger)
    : ICommandHandler<TCommand, TResult>
    where TCommand : ICommand
{
    public async Task<TResult> Handle(TCommand command)
    {
        using var ts = transactionContext.CreateTransactionScope();
        logger.LogInformation($"Method {command.ToString() ?? string.Empty} is used");
        var result = await handler.Handle(command);
        await transactionContext.SaveAsync();
        ts.Complete();
        return result;
    }
}