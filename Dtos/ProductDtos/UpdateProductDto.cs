using System.ComponentModel.DataAnnotations;

namespace Products.Api.Dtos.ProductDtos;

public class UpdateProductDto
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int CategoryId { get; set; }
}