using FinancialApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialApi.Infrastructure.Data.Configuration;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Account");

        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.AccountNumber).IsUnique();
        builder.Property(e => e.Branch).IsRequired().HasMaxLength(3);
        builder.Property(e => e.AccountNumber).IsRequired().HasMaxLength(9);
        builder.Property(e => e.Balance).HasPrecision(18, 2);
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt).IsRequired();
        builder.HasOne(e => e.Person).WithMany(p => p.Accounts).HasForeignKey(e => e.PersonId);
    }
}