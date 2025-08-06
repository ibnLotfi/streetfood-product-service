using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Enums;

namespace Product.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Domain.Entities.Product>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Product> builder)
    {
        // Table
        builder.ToTable("Products");

        // Primary Key
        builder.HasKey(p => p.Id);

        // Properties
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Description)
            .HasMaxLength(500);

        builder.Property(p => p.Category)
            .IsRequired()
            .HasConversion<string>() // Enum vers string dans la DB
            .HasMaxLength(50);

        builder.Property(p => p.BasePrice)
            .IsRequired()
            .HasPrecision(10, 2); // 10 chiffres dont 2 décimales

        builder.Property(p => p.ImageUrl)
            .HasMaxLength(500);

        builder.Property(p => p.Size)
            .HasMaxLength(20);

        builder.Property(p => p.MenuPrice)
            .HasPrecision(10, 2);

        // Value objects - Sérialiser les listes en JSON
        builder.Property(p => p.AvailableSizes)
            .HasConversion(
                v => string.Join(',', v.Select(s => s.ToString())),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                      .Select(s => Enum.Parse<ProductSize>(s))
                      .ToList()
            )
            .HasMaxLength(50);

        // Pour les IDs de relations
        builder.Property(p => p.AvailableSupplementIds)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                      .Select(Guid.Parse)
                      .ToList()
            );

        // Index pour les recherches
        builder.HasIndex(p => p.Category);
        builder.HasIndex(p => p.IsActive);
        builder.HasIndex(p => new { p.Category, p.IsActive });

        // Ignorer les domain events (pas stockés en DB)
        builder.Ignore(p => p.DomainEvents);
    }
}