using System.Transactions;

namespace Core.UseCases.TechnicalStuff.Transactions;

public interface ITransactionContext
{
  TransactionScope CreateTransactionScope();
    Task SaveAsync(CancellationToken cancellationToken = default);
}