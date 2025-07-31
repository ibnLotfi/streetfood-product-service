using Product.Domain.Common;
using Product.Domain.Enums;
using Product.Domain.Events;

namespace Product.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public ProductCategory Category { get; private set; }
    public decimal BasePrice { get; private set; }
    public string? ImageUrl { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsCustomizable { get; private set; }
    
    // Value Objects
    public List<ProductSize> AvailableSizes { get; private set; } = new();
    public List<Guid> AvailableSupplementIds { get; private set; } = new();
    public List<Guid> AvailableMeatIds { get; private set; } = new();
    public List<Guid> AvailableSauceIds { get; private set; } = new();
    public List<Guid> AvailableBaseIds { get; private set; } = new();
    
    // Pour les produits simples comme les boissons
    public string? Size { get; private set; }
    public decimal? MenuPrice { get; private set; }
    
    // Constructeur privé pour forcer l'utilisation de la factory method
    private Product() { }
    
    // Factory method
    public static Product Create(
        string name,
        ProductCategory category,
        decimal basePrice,
        bool isCustomizable = false)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be empty", nameof(name));
            
        if (basePrice < 0)
            throw new ArgumentException("Price cannot be negative", nameof(basePrice));
        
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
            Category = category,
            BasePrice = basePrice,
            IsCustomizable = isCustomizable,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
        
        // Raise domain event
        product.AddDomainEvent(new ProductCreatedEvent(product.Id, product.Name));
        
        return product;
    }
    
    // Méthodes métier
    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice < 0)
            throw new ArgumentException("Price cannot be negative");
            
        BasePrice = newPrice;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void Activate() 
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void SetAvailableSizes(List<ProductSize> sizes)
    {
        AvailableSizes = sizes ?? new List<ProductSize>();
        UpdatedAt = DateTime.UtcNow;
    }
}