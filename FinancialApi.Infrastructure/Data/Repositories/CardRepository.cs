using FinancialApi.Domain.Entities;
using FinancialApi.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinancialApi.Infrastructure.Data.Repositories;

public class CardRepository : ICardRepository
{
    private readonly AppDbContext _context;

    public CardRepository(AppDbContext context)
    {
        _context = context;
    }

    #region SELECT

    public async Task<int> CountCardsAsync(Guid PersonId)
    {
        return await _context.Cards.CountAsync(c => c.Account.PersonId == PersonId);
    }

    public async Task<bool> ExistsPhysicalCardAsync(Guid accountId)
    {
        return await _context.Cards.AnyAsync(c => c.AccountId == accountId && c.Type == "physical");
    }

    public async Task<List<Card>> GetByAccountIdAsync(Guid accountId)
    {
        return await _context.Cards.Where(c => c.AccountId == accountId).ToListAsync();
    }

    public async Task<List<Card>> GetByPersonIdAsync(Guid personId, int itemsPerPage, int currentPage)
    {
        return await _context.Cards
            .Where(c => c.Account.PersonId == personId)
            .OrderByDescending(c => c.CreatedAt)
            .Skip((currentPage - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .ToListAsync();
    }

    #endregion

    #region CREATE

    public async Task AddAsync(Card card)
    {
        await _context.Cards.AddAsync(card);
        await _context.SaveChangesAsync();
    }

    #endregion
}