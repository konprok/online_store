using OfferService.Database.Entities;

namespace OfferService.Models;

public sealed class OfferResponse
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public string? Description { get; set; }
    public Uri? Logo { get; set; }
    public double Price { get; set; }

    public OfferResponse(OfferEntity offerEntity)
    {
        Id = offerEntity.Id;
        Title = offerEntity.Title;
        Description = offerEntity.Description;
        Logo = offerEntity.Logo;
        Price = offerEntity.Price;
    }

    public OfferResponse()
    {
        
    }
}