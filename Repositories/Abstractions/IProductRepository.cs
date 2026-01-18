using Products.Api.Models;

namespace Products.Api.Repositories.Abstractions;

public interface IProductRepository
{
    Task<List<Product>> GetPagedAsync(
        int page,
        int pageSize,
        string? search,
        decimal? minPrice,
        decimal? maxPrice);

    Task<int> CountAsync(
        string? search,
        decimal? minPrice,
        decimal? maxPrice);

    Task<Product?> GetByIdAsync(int id);
    Task AddAsync(Product product);
    void Remove(Product product);
    Task SaveChangesAsync();
}
