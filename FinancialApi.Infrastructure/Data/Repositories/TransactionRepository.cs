using FinancialApi.Domain.Entities;
using FinancialApi.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinancialApi.Infrastructure.Data.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    #region SELECT

    public async Task<int> CountTransactionsAsync(Guid accountId, string? type)
    {
        return await _context.Transactions
            .Where(t => t.AccountId == accountId)
        .CountAsync(t => string.IsNullOrEmpty(type) || (type.ToLower() == "credit" ? t.Value > 0 : t.Value < 0));
    }

    public async Task<Transaction> GetByIdAsync(Guid id)
    {
        return await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<Transaction>> GetByAccountIdAsync(Guid accountId, int itemsPerPage, int currentPage, string? type)
    {
        var query = _context.Transactions
            .Where(t => t.AccountId == accountId);

        if (!string.IsNullOrEmpty(type))
        {
            query = type.ToLower() == "credit"
                ? query.Where(t => t.Value > 0)
                : query.Where(t => t.Value < 0);
        }

        return await query
            .OrderByDescending(t => t.CreatedAt)
            .Skip((currentPage - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .ToListAsync();
    }

    #endregion

    #region CREATE

    public async Task AddAsync(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
    }

    #endregion
}