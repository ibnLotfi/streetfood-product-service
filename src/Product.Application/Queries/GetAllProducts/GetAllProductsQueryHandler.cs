using AutoMapper;
using MediatR;
using Product.Application.DTOs;
using Product.Domain.Repositories;

namespace Product.Application.Queries.GetAllProducts;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync(cancellationToken);
        
        // Filtrer si demandé
        if (request.OnlyActive == true)
        {
            products = products.Where(p => p.IsActive);
        }
        
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }
}