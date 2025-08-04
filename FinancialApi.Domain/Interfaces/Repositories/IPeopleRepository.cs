using FinancialApi.Domain.Entities;

namespace FinancialApi.Domain.Interfaces.Repositories;

public interface IPeopleRepository
{
    #region SELECT

    Task<bool> ExistsByDocumentAsync(string document);
    Task<People> GetByDocumentAsync(string document);
    Task<People> GetByIdAsync(Guid id);

    #endregion

    #region CREATE

    Task AddAsync(People person);

    #endregion
}