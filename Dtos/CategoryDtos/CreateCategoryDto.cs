using System.ComponentModel.DataAnnotations;

namespace Products.Api.Dtos.CategoryDtos;

public class CreateCategoryDto
{
    [Required]
    public string Name { get; set; } = null!;
}