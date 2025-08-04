using FinancialApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialApi.Infrastructure.Data.Configuration;

public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.ToTable("Card");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Type).IsRequired();
        builder.Property(e => e.Number).IsRequired();
        builder.Property(e => e.Cvv).IsRequired().HasMaxLength(3);
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt).IsRequired();
        builder.HasOne(e => e.Account).WithMany(a => a.Cards).HasForeignKey(e => e.AccountId);
    }
}