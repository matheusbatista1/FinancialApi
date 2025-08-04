using FinancialApi.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FinancialApi.Infrastructure.Data.Configuration;

public class PersonConfiguration : IEntityTypeConfiguration<People>
{
    public void Configure(EntityTypeBuilder<People> builder)
    {
        builder.ToTable("People");

        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.Document).IsUnique();
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.Document).IsRequired();
        builder.Property(e => e.PasswordHash).IsRequired();
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt).IsRequired();
    }
}