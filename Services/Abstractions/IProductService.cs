using Products.Api.Dtos.ProductDtos;
using Products.Api.Responses;

namespace Products.Api.Services.Abstractions;

public interface IProductService
{
    Task<PagedResult<ReadProductDto>> GetPagedAsync(
        int page,
        int pageSize,
        string? search,
        decimal? minPrice,
        decimal? maxPrice);

    Task<ReadProductDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(CreateProductDto dto);
    Task UpdateAsync(int id, UpdateProductDto dto);
    Task DeleteAsync(int id);
}
