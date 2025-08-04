using FinancialApi.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FinancialApi.Infrastructure.Data.Configuration;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Card");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Value).HasPrecision(18, 2).IsRequired();
        builder.Property(e => e.Description).IsRequired();
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt).IsRequired();
        builder.Property(e => e.IsReverted).IsRequired();
        builder.HasOne(e => e.Account).WithMany(a => a.Transactions).HasForeignKey(e => e.AccountId);
    }
}