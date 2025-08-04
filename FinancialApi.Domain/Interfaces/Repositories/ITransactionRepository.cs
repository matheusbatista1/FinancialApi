using FinancialApi.Domain.Entities;

namespace FinancialApi.Domain.Interfaces.Repositories;

public interface ITransactionRepository
{
    #region SELECT

    Task<int> CountTransactionsAsync(Guid accountId, string? type);
    Task<Transaction> GetByIdAsync(Guid id);
    Task<List<Transaction>> GetByAccountIdAsync(Guid accountId, int itemsPerPage, int currentPage, string? type);

    #endregion

    #region CREATE

    Task AddAsync(Transaction transaction);

    #endregion
}