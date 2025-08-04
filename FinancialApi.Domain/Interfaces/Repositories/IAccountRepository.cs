using FinancialApi.Domain.Entities;

namespace FinancialApi.Domain.Interfaces.Repositories;

public interface IAccountRepository
{
    #region SELECT

    Task<bool> ExistsByAccountNumberAsync(string accountNumber);
    Task<Account> GetByIdAsync(Guid id);
    Task<List<Account>> GetByPersonIdAsync(Guid personId);
    Task<decimal> GetBalanceAsync(Guid accountId);

    #endregion

    #region CREATE

    Task AddAsync(Account account);

    #endregion

    #region UPDATE

    Task UpdateAsync(Account account);

    #endregion
}