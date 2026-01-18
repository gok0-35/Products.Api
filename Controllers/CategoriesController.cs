using Microsoft.AspNetCore.Mvc;
using Products.Api.Dtos.CategoryDtos;
using Products.Api.Responses;
using Products.Api.Services.Abstractions;

namespace Products.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
public sealed class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // GET api/categories
    [HttpGet]
    [ProducesResponseType(typeof(List<ReadCategoryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ReadCategoryDto>>> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();
        return Ok(categories);
    }

    // GET api/categories/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ReadCategoryDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<ReadCategoryDto>> GetById(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        return Ok(category);
    }

    // POST api/categories
    [HttpPost]
    [ProducesResponseType(typeof(ReadCategoryDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<ReadCategoryDto>> Create(
        [FromBody] CreateCategoryDto dto)
    {
        var created = await _categoryService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = created.Id },
            created);
    }

    // PUT api/categories/5
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateCategoryDto dto)
    {
        await _categoryService.UpdateAsync(id, dto);
        return NoContent();
    }

    // DELETE api/categories/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        await _categoryService.DeleteAsync(id);
        return NoContent();
    }
}
