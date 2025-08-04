using FinancialApi.Domain.Entities;
using FinancialApi.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinancialApi.Infrastructure.Data.Repositories;

public class PeopleRepository : IPeopleRepository
{
    private readonly AppDbContext _context;

    public PeopleRepository(AppDbContext context)
    {
        _context = context;
    }

    #region SELECT

    public async Task<bool> ExistsByDocumentAsync(string document)
    {
        return await _context.Peoples.AnyAsync(p => p.Document == document);
    }

    public async Task<People> GetByDocumentAsync(string document)
    {
        return await _context.Peoples.FirstOrDefaultAsync(p => p.Document == document);
    }

    public async Task<People> GetByIdAsync(Guid id)
    {
        return await _context.Peoples.FirstOrDefaultAsync(p => p.Id == id);
    }

    #endregion

    #region CREATE

    public async Task AddAsync(People person)
    {
        _context.Peoples.Add(person);
        await _context.SaveChangesAsync();
    }

    #endregion
}