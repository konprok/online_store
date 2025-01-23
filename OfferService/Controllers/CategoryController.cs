using Microsoft.AspNetCore.Mvc;
using OfferService.Database.Entities;
using OfferService.Models;
using OfferService.Models.Exceptions;
using OfferService.Services.Interfaces;

namespace OfferService.Controllers;

[ApiController]
//[Route("category")]
public sealed class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    [HttpPost("/user/{userId:Guid}/category")]
    public async Task<ActionResult<CategoryEntity>> PostCategory(Guid userId, [FromBody] Category category)
    {
        try
        {
            return Ok(await _categoryService.PostCategory(userId, category));
        }
        catch (CategoryAlreadyExist ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet("/categories")]
    public async Task<ActionResult<CategoryEntity>> GetCategories()
    {
        try
        {
            return Ok(await _categoryService.GetCategories());
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}