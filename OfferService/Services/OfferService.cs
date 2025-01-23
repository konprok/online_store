using OfferService.ApiReference;
using OfferService.ApiReference.ResponseModels;
using OfferService.Database.Entities;
using OfferService.Database.Repositories.Interfaces;
using OfferService.Models;
using OfferService.Services.Interfaces;

namespace OfferService.Services;

public sealed class OfferService : IOfferService
{
    private readonly IOfferRepository _offerRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUsersApiReference _usersApiReference;
    
    public OfferService(IOfferRepository offerRepository, ICategoryRepository categoryRepository, IUsersApiReference usersApiReference)
    {
        _offerRepository = offerRepository;
        _categoryRepository = categoryRepository;
        _usersApiReference = usersApiReference;
    }
    
    public async Task<OfferEntity> PostOffer(Guid userId, Offer offer)
    {
        OfferEntity offerEntity = new(offer)
        {
            Id = new long(),
            CreatedAt = DateTimeOffset.UtcNow,
            CreatedBy = userId,
            Logo = offer.Logo
        };

        await _offerRepository.InsertOfferAsync(offerEntity);
        await _offerRepository.SaveAsync();

        return offerEntity;
    }
    
    public async Task<OfferEntity> PatchOffer(Guid userId, long offerId, Offer offer)
    {

        CategoryEntity categoryEntity = await _categoryRepository.GetCategoryById(offer.Category);
        
        OfferEntity offerEntity = await _offerRepository.GetOffer(userId, offerId);
        offerEntity.Title = offer.Title;
        offerEntity.CategoryId = categoryEntity.Id;
        offerEntity.Description = offer.Description;
        offerEntity.Price = offer.Price;
        offerEntity.ModifiedAt = DateTimeOffset.UtcNow;
        await _offerRepository.SaveAsync();

        return offerEntity;
    }

    public async Task<IEnumerable<OfferResponse>> GetUsersOffers(Guid userId)
    {
        IEnumerable<OfferEntity> offers = await _offerRepository.GetUsersOffers(userId);
        return await ConvertObjects(offers);
    }

    public async Task<IEnumerable<OfferResponse>> GetOffers()
    {
        IEnumerable<OfferEntity> offers = await _offerRepository.GetOffers();
        return await ConvertObjects(offers);
    }

    public async Task<OfferEntity> GetOffer(long offerId)
    {
        return await _offerRepository.GetOffer(offerId);
    }

    public async Task<List<OfferResponse>> GetOffersByIds(List<long> offerIds)
    {
        return await _offerRepository.GetOffersByIds(offerIds);
    }

    private async Task<IEnumerable<OfferResponse>> ConvertObjects(IEnumerable<OfferEntity> offerEntities) //no ta metoda mi sie nie podoba w wolnym czasie refactor
    {
        var offerResponses = new List<OfferResponse>();
        foreach (var offer in offerEntities)
        {
            var categoryEntity = await _categoryRepository.GetCategoryById(offer.CategoryId);
            var offerResponse = new OfferResponse(offer)
            {
                Category = categoryEntity.Name
            };
            offerResponses.Add(offerResponse);
        }

        return offerResponses;
    }

    public async Task<IEnumerable<OfferResponse>> GetOfferByCategory(int categoryId)
    {
        return await _offerRepository.GetOfferByCategory(categoryId);
    }
    
    public async Task<bool> DeleteOffer(Guid userId, long offerId)
    {
        OfferEntity offerEntity = await _offerRepository.GetOffer(offerId);
        var userResponse = await _usersApiReference.GetUserById(userId);
        if (offerEntity.CreatedBy == userId || userResponse.IsAdmin)
        {
            offerEntity.IsDeleted = true;
            await _offerRepository.SaveAsync();

            return true;
        }

        return false;
    }
    public async Task<bool> DeleteOffers(Guid userId)
    {
        var offers = await _offerRepository.GetOffers(userId);
        offers.ForEach(offer => offer.IsDeleted = true);
        await _offerRepository.SaveAsync();
        return true;
    }

}