using Microsoft.Extensions.Logging;
using Products.Api.Dtos.CategoryDtos;
using Products.Api.Exceptions;
using Products.Api.Models;
using Products.Api.Repositories.Abstractions;
using Products.Api.Services.Abstractions;

namespace Products.Api.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(
        ICategoryRepository categoryRepository,
        ILogger<CategoryService> logger)
    {
        _categoryRepository = categoryRepository;
        _logger = logger;
    }

    public async Task<List<ReadCategoryDto>> GetAllAsync()
    {
        _logger.LogInformation("Fetching all categories");

        var categories = await _categoryRepository.GetAllAsync();

        _logger.LogInformation(
            "Fetched {CategoryCount} categories",
            categories.Count);

        return categories.Select(MapToReadDto).ToList();
    }

    public async Task<ReadCategoryDto> GetByIdAsync(int id)
    {
        _logger.LogInformation(
            "Fetching category by id. CategoryId: {CategoryId}",
            id);

        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
        {
            _logger.LogWarning(
                "Category not found. CategoryId: {CategoryId}",
                id);

            throw new NotFoundException("Kategori bulunamadı");
        }

        return MapToReadDto(category);
    }

    public async Task<ReadCategoryDto> CreateAsync(CreateCategoryDto dto)
    {
        _logger.LogInformation(
            "Creating category. Name: {CategoryName}",
            dto.Name);

        var category = new Category
        {
            Name = dto.Name
        };

        await _categoryRepository.AddAsync(category);

        _logger.LogInformation(
            "Category created successfully. CategoryId: {CategoryId}",
            category.Id);

        return MapToReadDto(category);
    }

    public async Task UpdateAsync(int id, UpdateCategoryDto dto)
    {
        _logger.LogInformation(
            "Updating category. CategoryId: {CategoryId}",
            id);

        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
        {
            _logger.LogWarning(
                "Update failed. Category not found. CategoryId: {CategoryId}",
                id);

            throw new NotFoundException("Kategori bulunamadı");
        }

        category.Name = dto.Name;

        await _categoryRepository.UpdateAsync(category);

        _logger.LogInformation(
            "Category updated successfully. CategoryId: {CategoryId}",
            id);
    }

    public async Task DeleteAsync(int id)
    {
        _logger.LogInformation(
            "Deleting category. CategoryId: {CategoryId}",
            id);

        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
        {
            _logger.LogWarning(
                "Delete failed. Category not found. CategoryId: {CategoryId}",
                id);

            throw new NotFoundException("Kategori bulunamadı");
        }

        await _categoryRepository.DeleteAsync(category);

        _logger.LogInformation(
            "Category deleted successfully. CategoryId: {CategoryId}",
            id);
    }

    private static ReadCategoryDto MapToReadDto(Category category)
    {
        return new ReadCategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }
}
