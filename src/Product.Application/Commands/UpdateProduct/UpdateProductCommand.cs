using MediatR;

namespace Product.Application.Commands.UpdateProduct;

public record UpdateProductCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public decimal? BasePrice { get; init; }
    public string? ImageUrl { get; init; }
    public bool? IsActive { get; init; }
}