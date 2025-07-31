using Product.Domain.Enums;

namespace Product.Domain.Repositories;

public interface IProductRepository
{
    Task<Entities.Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Entities.Product>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Entities.Product>> GetByCategoryAsync(ProductCategory category, CancellationToken cancellationToken = default);
    Task<Guid> AddAsync(Entities.Product product, CancellationToken cancellationToken = default);
    Task UpdateAsync(Entities.Product product, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}