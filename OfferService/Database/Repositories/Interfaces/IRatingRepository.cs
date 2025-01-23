using OfferService.Database.Entities;

namespace OfferService.Database.Repositories.Interfaces;

public interface IRatingRepository
{
    Task InsertRatingAsync(RatingEntity ratingEntity);
    Task SaveAsync();
    Task<IEnumerable<RatingEntity>> GetUsersRatings(Guid userId);
    Task<IEnumerable<RatingEntity>> GetRatings(long offerId);
}