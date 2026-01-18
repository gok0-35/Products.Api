using Microsoft.EntityFrameworkCore;
using Products.Api.Data;
using Products.Api.Models;
using Products.Api.Repositories.Abstractions;

namespace Products.Api.Repositories.EfCore;

public class ProductRepository : IProductRepository
{
    private readonly ProductsDbContext _context;

    public ProductRepository(ProductsDbContext context)
    {
        _context = context;
    }

    private IQueryable<Product> ApplyFilters(
        IQueryable<Product> query,
        string? search,
        decimal? minPrice,
        decimal? maxPrice)
    {
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(p => p.Name.Contains(search));

        if (minPrice.HasValue)
            query = query.Where(p => p.Price >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(p => p.Price <= maxPrice.Value);

        return query;
    }

    public async Task<List<Product>> GetPagedAsync(
        int page,
        int pageSize,
        string? search,
        decimal? minPrice,
        decimal? maxPrice)
    {
        var query = _context.Products
            .Include(p => p.Category)
            .AsQueryable();

        query = ApplyFilters(query, search, minPrice, maxPrice);

        return await query
            .OrderBy(p => p.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> CountAsync(
        string? search,
        decimal? minPrice,
        decimal? maxPrice)
    {
        var query = _context.Products.AsQueryable();
        query = ApplyFilters(query, search, minPrice, maxPrice);
        return await query.CountAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public void Remove(Product product)
    {
        _context.Products.Remove(product);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
