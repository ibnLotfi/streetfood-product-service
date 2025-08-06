using MediatR;
using Product.Application.DTOs;

namespace Product.Application.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto?>;