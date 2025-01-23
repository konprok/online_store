using OfferService.Models;

namespace OfferService.Database.Entities;

public sealed class OfferEntity
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public Uri? Logo;
    public double Price { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? ModifiedAt { get; set; }
    public bool IsDeleted { get; set; }
    public int CategoryId { get; set; }
    public ICollection<RatingEntity> Ratings { get; set; } = new List<RatingEntity>();
    // public CategoryEntity Category { get; set; }

    public OfferEntity(Offer offer)
    {
        Title = offer.Title;
        Description = offer.Description;
        Logo = offer.Logo;
        Price = offer.Price;
        CategoryId = offer.Category;
    }

    public OfferEntity() { }
}