using MediatR;
using Product.Application.DTOs;

namespace Product.Application.Queries.GetAllProducts;

public record GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>
{
    public bool? OnlyActive { get; init; } = true;
}