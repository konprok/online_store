using OfferService.Database.Entities;
using OfferService.Models;

namespace OfferService.Services.Interfaces;

public interface IRatingService
{
    Task<RatingResponse> PostRating(long offerId, Rating rating);
    Task<IEnumerable<RatingResponse>> GetRatingsByUserId(Guid userId);
    Task<IEnumerable<RatingResponse>> GetRatingsByOfferId(long offerId);
}