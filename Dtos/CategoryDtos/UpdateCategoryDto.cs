using System.ComponentModel.DataAnnotations;

namespace Products.Api.Dtos.CategoryDtos;

public class UpdateCategoryDto
{
    [Required]
    public string Name { get; set; } = null!;
}
