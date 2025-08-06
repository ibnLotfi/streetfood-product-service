using Microsoft.EntityFrameworkCore;
using Product.Domain.Common;
using System.Reflection;

namespace Product.Infrastructure.Persistence;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {
    }

    public DbSet<Domain.Entities.Product> Products => Set<Domain.Entities.Product>();
    public DbSet<Domain.Entities.ProductOption> ProductOptions => Set<Domain.Entities.ProductOption>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Appliquer toutes les configurations
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Gérer les dates de création/modification automatiquement
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    break;
                case EntityState.Modified:
                    entry.Entity.SetUpdatedAt(DateTime.UtcNow);
                    break;
            }
        }

        // Publier les domain events (si vous utilisez un event bus)
        // await DispatchDomainEvents();

        return await base.SaveChangesAsync(cancellationToken);
    }
}