using Microsoft.Extensions.Logging;
using Products.Api.Dtos.ProductDtos;
using Products.Api.Exceptions;
using Products.Api.Models;
using Products.Api.Repositories.Abstractions;
using Products.Api.Responses;
using Products.Api.Services.Abstractions;

namespace Products.Api.Services;

public class ProductService : IProductService
{
    private const int MaxPageSize = 50;

    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductService> _logger;

    public ProductService(
        IProductRepository productRepository,
        ILogger<ProductService> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    // ✅ PAGINATION + FILTERING
    public async Task<PagedResult<ReadProductDto>> GetPagedAsync(
        int page,
        int pageSize,
        string? search,
        decimal? minPrice,
        decimal? maxPrice)
    {
        page = page <= 0 ? 1 : page;
        pageSize = pageSize <= 0 ? 10 : Math.Min(pageSize, MaxPageSize);

        _logger.LogInformation(
            "Fetching products. Page: {Page}, PageSize: {PageSize}, Search: {Search}",
            page, pageSize, search);

        var totalCount = await _productRepository.CountAsync(
            search,
            minPrice,
            maxPrice);

        var products = await _productRepository.GetPagedAsync(
            page,
            pageSize,
            search,
            minPrice,
            maxPrice);

        _logger.LogInformation(
            "Fetched {ProductCount} products out of {TotalCount}",
            products.Count,
            totalCount);

        return new PagedResult<ReadProductDto>
        {
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            Items = products.Select(p => new ReadProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CategoryName = p.Category?.Name ?? string.Empty
            }).ToList()
        };
    }

    public async Task<ReadProductDto?> GetByIdAsync(int id)
    {
        _logger.LogInformation(
            "Fetching product by id. ProductId: {ProductId}",
            id);

        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            _logger.LogWarning(
                "Product not found. ProductId: {ProductId}",
                id);

            return null;
        }

        return new ReadProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            CategoryName = product.Category?.Name ?? string.Empty
        };
    }

    public async Task<int> CreateAsync(CreateProductDto dto)
    {
        _logger.LogInformation(
            "Creating product. Name: {Name}, Price: {Price}, CategoryId: {CategoryId}",
            dto.Name,
            dto.Price,
            dto.CategoryId);

        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            CategoryId = dto.CategoryId
        };

        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();

        _logger.LogInformation(
            "Product created successfully. ProductId: {ProductId}",
            product.Id);

        return product.Id;
    }

    public async Task UpdateAsync(int id, UpdateProductDto dto)
    {
        _logger.LogInformation(
            "Updating product. ProductId: {ProductId}",
            id);

        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            _logger.LogWarning(
                "Update failed. Product not found. ProductId: {ProductId}",
                id);

            throw new NotFoundException("Ürün bulunamadı");
        }

        product.Name = dto.Name;
        product.Price = dto.Price;
        product.CategoryId = dto.CategoryId;

        await _productRepository.SaveChangesAsync();

        _logger.LogInformation(
            "Product updated successfully. ProductId: {ProductId}",
            id);
    }

    public async Task DeleteAsync(int id)
    {
        _logger.LogInformation(
            "Deleting product. ProductId: {ProductId}",
            id);

        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            _logger.LogWarning(
                "Delete failed. Product not found. ProductId: {ProductId}",
                id);

            throw new NotFoundException("Ürün bulunamadı");
        }

        _productRepository.Remove(product);
        await _productRepository.SaveChangesAsync();

        _logger.LogInformation(
            "Product deleted successfully. ProductId: {ProductId}",
            id);
    }
}
