using Microsoft.EntityFrameworkCore;
using OfferService.Database.DbContext;
using OfferService.Database.Entities;
using OfferService.Database.Repositories.Interfaces;

namespace OfferService.Database.Repositories;

public class RatingRepository : IRatingRepository
{
    private readonly OfferDbContext _dbContext;

    public RatingRepository(OfferDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<RatingEntity>> GetRatings(long offerId)
    {
        return await _dbContext.Rating.Where(x => x.OfferId == offerId && !x.IsDeleted).ToListAsync();
    }
    
    public async Task<IEnumerable<RatingEntity>> GetUsersRatings(Guid userId)
    {
        return await _dbContext.Rating.Where(x => x.CreatedBy == userId && !x.IsDeleted).ToListAsync();
    }
    
    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task InsertRatingAsync(RatingEntity ratingEntity)
    {
        await _dbContext.Rating.AddAsync(ratingEntity);
    }
}