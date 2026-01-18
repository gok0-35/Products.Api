using System.ComponentModel.DataAnnotations;

namespace Products.Api.Dtos.ProductDtos;

public class CreateProductDto
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int CategoryId { get; set; }
}