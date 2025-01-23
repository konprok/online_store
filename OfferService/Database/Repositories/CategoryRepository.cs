using Microsoft.EntityFrameworkCore;
using OfferService.Database.DbContext;
using OfferService.Database.Entities;
using OfferService.Database.Repositories.Interfaces;
using OfferService.Models.Exceptions;

namespace OfferService.Database.Repositories;

public sealed class CategoryRepository : ICategoryRepository
{
    private readonly OfferDbContext _dbContext;

    public CategoryRepository(OfferDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CategoryEntity> GetCategoryByName(string categoryName)
    {
        CategoryEntity? categoryEntity = await _dbContext.Category.Where(x => x.Name == categoryName).SingleOrDefaultAsync();
        if (categoryEntity == null)
        {
            throw new CategoryDoesNotExistException();
        }

        return categoryEntity;
    }
    
    public async Task<CategoryEntity> GetCategoryById(int categoryId)
    {
        CategoryEntity? categoryEntity = await _dbContext.Category.Where(x => x.Id == categoryId).SingleOrDefaultAsync();
        if (categoryEntity == null)
        {
            throw new CategoryDoesNotExistException();
        }

        return categoryEntity;
    }

    public async Task<IEnumerable<CategoryEntity>> GetCategories()
    {
        return await _dbContext.Category.ToListAsync();
    }
    
    public async Task InsertCategoryAsync(CategoryEntity categoryEntity)
    {
        if (_dbContext.Category.Any(x => x.Name == categoryEntity.Name))
        {
            throw new CategoryAlreadyExist();
        }
        await _dbContext.Category.AddAsync(categoryEntity); 
    }
    
    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
    
}