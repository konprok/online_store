using OfferService.Database.Entities;
using OfferService.Models;

namespace OfferService.Database.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<CategoryEntity> GetCategoryByName(string categoryName);
    Task<CategoryEntity> GetCategoryById(int categoryId);
    Task<IEnumerable<CategoryEntity>> GetCategories();
    Task InsertCategoryAsync(CategoryEntity categoryEntity);
    Task SaveAsync();
}