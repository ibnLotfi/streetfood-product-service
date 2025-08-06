using MediatR;
using Product.Domain.Repositories;
using Product.Application.Exceptions;

namespace Product.Application.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (product == null)
            throw new NotFoundException($"Product with ID {request.Id} not found");
        
        // Mettre à jour seulement les champs fournis
        if (!string.IsNullOrEmpty(request.Name))
        {
            // Vous devrez ajouter une méthode UpdateName dans l'entité Domain
            // product.UpdateName(request.Name);
        }
        
        if (request.BasePrice.HasValue)
        {
            product.UpdatePrice(request.BasePrice.Value);
        }
        
        if (request.IsActive.HasValue)
        {
            if (request.IsActive.Value)
                product.Activate();
            else
                product.Deactivate();
        }
        
        await _productRepository.UpdateAsync(product, cancellationToken);
        
        return Unit.Value; // Pas de valeur de retour pour les updates
    }
}