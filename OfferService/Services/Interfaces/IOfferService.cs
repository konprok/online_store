using OfferService.Database.Entities;
using OfferService.Models;

namespace OfferService.Services.Interfaces;
public interface IOfferService
{
    Task<OfferEntity> PostOffer(Guid userId, Offer offer);
    Task<OfferEntity> GetOffer(long offerId);
    Task<List<OfferResponse>> GetOffersByIds(List<long> offerIds);
    Task<IEnumerable<OfferResponse>> GetOffers();
    Task<IEnumerable<OfferResponse>> GetUsersOffers(Guid userId);
    Task<bool> DeleteOffer(Guid userId, long offerId);
    Task<OfferEntity> PatchOffer(Guid userId, long offerId, Offer offer);
    Task<IEnumerable<OfferResponse>> GetOfferByCategory(int categoryId);
    Task<bool> DeleteOffers(Guid userId);
}