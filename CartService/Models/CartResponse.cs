using CartService.Models.OfferServiceModels;

namespace CartService.Models;

public sealed class CartResponse
{
    public List<OffersResponse> Offers { get; set; }
    public double Price { get; set; }

    public CartResponse(List<OffersResponse> offers)
    {
        Offers = offers;
        foreach (var offer in offers)
        {
            Price += offer.Price;
        }
    }

    public CartResponse()
    {
        
    }
}