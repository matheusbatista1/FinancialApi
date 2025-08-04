using FinancialApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinancialApi.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<People> Peoples { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.Entity<People>().ToTable("people");
        modelBuilder.Entity<Account>().ToTable("account");
        modelBuilder.Entity<Card>().ToTable("card");
        modelBuilder.Entity<Transaction>().ToTable("transaction");
    }
}