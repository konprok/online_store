using OfferService.Database.Entities;
using OfferService.Database.Repositories.Interfaces;
using OfferService.Models;
using OfferService.Services.Interfaces;

namespace OfferService.Services;

public sealed class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryEntity> PostCategory(Guid userId, Category category)
    {
        //tutaj refit do userservice po param is_admin
        CategoryEntity categoryEntity = new(category);
        await _categoryRepository.InsertCategoryAsync(categoryEntity);
        await _categoryRepository.SaveAsync();
        return categoryEntity;
    }

    public async Task<IEnumerable<CategoryEntity>> GetCategories()
    {
        return await _categoryRepository.GetCategories();
    }
    
}