using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Products.Api.Models;

namespace Products.Api.Data;

public class ProductsDbContext : DbContext
{
    public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
    {

    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

}