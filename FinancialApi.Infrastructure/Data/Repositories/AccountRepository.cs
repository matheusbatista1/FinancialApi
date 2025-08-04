using FinancialApi.Domain.Entities;
using FinancialApi.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinancialApi.Infrastructure.Data.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;

    public AccountRepository(AppDbContext context)
    {
        _context = context;
    }

    #region SELECT

    public async Task<bool> ExistsByAccountNumberAsync(string accountNumber)
    {
        return await _context.Accounts.AnyAsync(a => a.AccountNumber == accountNumber);
    }

    public async Task<Account> GetByIdAsync(Guid id)
    {
        return await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<Account>> GetByPersonIdAsync(Guid personId)
    {
        return await _context.Accounts.Where(a => a.PersonId == personId).ToListAsync();
    }

    public async Task<decimal> GetBalanceAsync(Guid accountId)
    {
        return await _context.Accounts.Where(a => a.Id == accountId).Select(a => a.Balance).FirstOrDefaultAsync();
    }

    #endregion

    #region CREATE

    public async Task AddAsync(Account account)
    {
        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
    }

    #endregion

    #region CREATE

    public async Task UpdateAsync(Account account)
    {
        _context.Accounts.Update(account);
        await _context.SaveChangesAsync();
    }

    #endregion
}