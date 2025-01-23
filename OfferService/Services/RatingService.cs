using Microsoft.AspNetCore.Http.HttpResults;
using OfferService.ApiReference;
using OfferService.Database.Entities;
using OfferService.Database.Repositories.Interfaces;
using OfferService.Models;
using OfferService.Models.Exceptions;
using OfferService.Services.Interfaces;


namespace OfferService.Services;

public class RatingService : IRatingService
{
    private readonly IUsersApiReference _usersApiReference;
    private readonly IRatingRepository _ratingRepository;
    private readonly IOfferService _offerService;

    public RatingService(IRatingRepository ratingRepository, IUsersApiReference usersApiReference, IOfferService offerService)
    {
        _ratingRepository = ratingRepository;
        _usersApiReference = usersApiReference;
        _offerService = offerService;
    }

    public async Task<RatingResponse> PostRating(long offerId, Rating rating)
    {
        string username;
        
        try
        {
            var user = await _usersApiReference.GetUserById(rating.CreatedBy);
            username = user.Name;
        }
        catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            throw new UserNotFoundException("User not found.");
        }
        
        var offer = await _offerService.GetOffer(offerId);
        if (offer.CreatedBy == rating.CreatedBy)
        {
            throw new InvalidRatingException("You cannot rate your own offer.");
        }
        
        var ratingExists = await GetRatingsByOfferId(offerId);
        if (ratingExists.Any(x => x.CreatedBy == rating.CreatedBy))
        {
            throw new RatingAlreadyExistException("You have already rated this offer.");
        }

        var ratingEntity = new RatingEntity(offerId, rating)
        {
            CreatedAt = DateTimeOffset.UtcNow,
            ModifiedAt = DateTimeOffset.UtcNow
        };
        
        await _ratingRepository.InsertRatingAsync(ratingEntity);
        await _ratingRepository.SaveAsync();
        
        var response = ratingEntity.ToRatingResponse();
        response.Username = username;

        return response;
    }



    
    public async Task<IEnumerable<RatingResponse>> GetRatingsByUserId(Guid userId)
    {
        IEnumerable<RatingEntity> ratings = await _ratingRepository.GetUsersRatings(userId);
        return ratings.Select(rating => rating.ToRatingResponse());
    }
    
    public async Task<IEnumerable<RatingResponse>> GetRatingsByOfferId(long offerId)
    {
        IEnumerable<RatingEntity> ratings = await _ratingRepository.GetRatings(offerId);

        var ratingResponses = await Task.WhenAll(ratings.Select(async rating =>
        {
            var ratingResponse = rating.ToRatingResponse();
            try
            {
                var user = await _usersApiReference.GetUserById(ratingResponse.CreatedBy);
                ratingResponse.Username = user.Name;
            }
            catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                ratingResponse.Username = "Unknown";
            }
            return ratingResponse;
        }));

        return ratingResponses;
    }

}