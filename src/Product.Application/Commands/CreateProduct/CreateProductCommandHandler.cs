using MediatR;
using Product.Domain.Repositories;
using Product.Domain.Enums;

namespace Product.Application.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Convertir la string category en enum
        if (!Enum.TryParse<ProductCategory>(request.Category, true, out var category))
        {
            throw new ArgumentException($"Invalid category: {request.Category}");
        }

        // Créer le produit via la factory method
        var product = Domain.Entities.Product.Create(
            request.Name,
            category,
            request.BasePrice,
            request.IsCustomizable
        );

        // Ajouter les propriétés optionnelles
        if (!string.IsNullOrEmpty(request.Description))
        {
            // TODO
        }

        if (request.AvailableSizes?.Any() == true)
        {
            var sizes = request.AvailableSizes
                .Select(s => Enum.Parse<ProductSize>(s, true))
                .ToList();
            product.SetAvailableSizes(sizes);
        }

        // Sauvegarder
        var id = await _productRepository.AddAsync(product, cancellationToken);
        
        return id;
    }
}