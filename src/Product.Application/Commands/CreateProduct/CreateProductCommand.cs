using MediatR;
using Product.Application.DTOs;

namespace Product.Application.Commands.CreateProduct;

public record CreateProductCommand : IRequest<Guid>
{
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string Category { get; init; } = string.Empty;
    public decimal BasePrice { get; init; }
    public string? ImageUrl { get; init; }
    public bool IsCustomizable { get; init; }
    public List<string>? AvailableSizes { get; init; }
    public string? Size { get; init; }
    public decimal? MenuPrice { get; init; }
}
