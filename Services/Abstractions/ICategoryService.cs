using Products.Api.Dtos.CategoryDtos;

namespace Products.Api.Services.Abstractions;

public interface ICategoryService
{
    Task<List<ReadCategoryDto>> GetAllAsync();
    Task<ReadCategoryDto> GetByIdAsync(int id);
    Task<ReadCategoryDto> CreateAsync(CreateCategoryDto dto);
    Task UpdateAsync(int id, UpdateCategoryDto dto);
    Task DeleteAsync(int id);
}
