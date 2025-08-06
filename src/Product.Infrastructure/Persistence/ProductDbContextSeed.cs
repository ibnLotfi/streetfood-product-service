using Microsoft.EntityFrameworkCore;
using Product.Domain.Enums;

namespace Product.Infrastructure.Persistence;

public static class ProductDbContextSeed
{
    public static async Task SeedAsync(ProductDbContext context)
    {
        if (!await context.Products.AnyAsync())
        {
            var products = new[]
            {
                Domain.Entities.Product.Create("Tacos sur mesure", ProductCategory.Tacos, 6m, true),
                Domain.Entities.Product.Create("Tacos Poulet", ProductCategory.Tacos, 7m),
                Domain.Entities.Product.Create("Pizza sur mesure", ProductCategory.Pizza, 8m, true),
                Domain.Entities.Product.Create("Coca-Cola", ProductCategory.Drink, 2.5m),
                Domain.Entities.Product.Create("Tiramisu", ProductCategory.Dessert, 4m)
            };

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }
}