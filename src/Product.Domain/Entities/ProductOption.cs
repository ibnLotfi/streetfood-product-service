using Product.Domain.Common;

namespace Product.Domain.Entities;

public class ProductOption : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public OptionType Type { get; private set; }
    public decimal Price { get; private set; }
    public bool IsAvailable { get; private set; }
    
    // Pour les sauces qui peuvent être en extra
    public bool CanBeExtra { get; private set; }
    public decimal? ExtraPrice { get; private set; }
    
    private ProductOption() { }
    
    public static ProductOption Create(
        string name,
        OptionType type,
        decimal price = 0,
        bool canBeExtra = false,
        decimal? extraPrice = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Option name cannot be empty", nameof(name));
            
        return new ProductOption
        {
            Id = Guid.NewGuid(),
            Name = name,
            Type = type,
            Price = price,
            IsAvailable = true,
            CanBeExtra = canBeExtra,
            ExtraPrice = extraPrice,
            CreatedAt = DateTime.UtcNow
        };
    }
}

public enum OptionType
{
    Meats,
    Sauces,
    Supplements,
    Bases,
    Size
}