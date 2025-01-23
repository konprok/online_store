using CartService.Models.OfferServiceModels;
using Refit;

namespace CartService.ApiReferences;

public interface IOfferApiReference
{
    [Post("/offers")]
    Task<List<OffersResponse>> GetOffersByIds([Body] List<long> offerIds);
}