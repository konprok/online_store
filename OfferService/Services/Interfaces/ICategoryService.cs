using OfferService.Database.Entities;
using OfferService.Models;

namespace OfferService.Services.Interfaces;

public interface ICategoryService
{
    Task<CategoryEntity> PostCategory(Guid userId, Category category);
    Task<IEnumerable<CategoryEntity>> GetCategories();
}