namespace Product.Application.DTOs;

public record ProductDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string Category { get; init; } = string.Empty;
    public decimal BasePrice { get; init; }
    public string? ImageUrl { get; init; }
    public bool IsActive { get; init; }
    public bool IsCustomizable { get; init; }
    public List<string> AvailableSizes { get; init; } = new();
    public string? Size { get; init; }
    public decimal? MenuPrice { get; init; }
    public DateTime CreatedAt { get; init; }
}