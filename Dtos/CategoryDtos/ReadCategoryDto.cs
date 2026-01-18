using System.ComponentModel.DataAnnotations;

namespace Products.Api.Dtos.CategoryDtos;

public class ReadCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}