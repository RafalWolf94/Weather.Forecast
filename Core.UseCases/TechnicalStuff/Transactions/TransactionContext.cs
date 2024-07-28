using System.Transactions;

namespace Core.UseCases.TechnicalStuff.Transactions;

public class TransactionContext : ITransactionContext
{
    public TransactionScope CreateTransactionScope()
    {
        return new TransactionScope(TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled);
    }

    public Task SaveAsync(CancellationToken cancellationToken = default)
        => Task.CompletedTask;
}