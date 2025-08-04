using FinancialApi.Domain.Entities;

namespace FinancialApi.Domain.Interfaces.Repositories;

public interface ICardRepository
{
    #region SELECT

    Task<int> CountCardsAsync(Guid PersonId);
    Task<bool> ExistsPhysicalCardAsync(Guid accountId);
    Task<List<Card>> GetByAccountIdAsync(Guid accountId);
    Task<List<Card>> GetByPersonIdAsync(Guid personId, int itemsPerPage, int currentPage);

    #endregion

    #region CREATE

    Task AddAsync(Card card);

    #endregion
}