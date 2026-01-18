using Microsoft.AspNetCore.Mvc;
using Products.Api.Dtos.ProductDtos;
using Products.Api.Responses;
using Products.Api.Services.Abstractions;

namespace Products.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
public sealed class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    // GET api/products?page=1&pageSize=10&search=iphone
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<ReadProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResult<ReadProductDto>>> GetPaged(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? search = null,
        [FromQuery] decimal? minPrice = null,
        [FromQuery] decimal? maxPrice = null)
    {
        var result = await _productService.GetPagedAsync(
            page,
            pageSize,
            search,
            minPrice,
            maxPrice);

        return Ok(result);
    }

    // GET api/products/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ReadProductDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<ReadProductDto>> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        return Ok(product);
    }

    // POST api/products
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(
        [FromBody] CreateProductDto dto)
    {
        var id = await _productService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            null);
    }

    // PUT api/products/5
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateProductDto dto)
    {
        await _productService.UpdateAsync(id, dto);
        return NoContent();
    }

    // DELETE api/products/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.DeleteAsync(id);
        return NoContent();
    }
}
