using OfferService.Models;

namespace OfferService.Database.Entities;

public sealed class RatingEntity
{
    public long Id { get; set; }
    public long OfferId { get; set; }
    public string? Description { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? ModifiedAt { get; set; }
    public bool IsDeleted { get; set; }
    public string Title { get; set; }
    public Rate Rating { get; set; }
    public OfferEntity Offer { get; set; }

    public RatingEntity(long offerId, Rating rating)
    {
        OfferId = offerId;
        CreatedBy = rating.CreatedBy;
        Id = new long();
        Description = rating.Description;
        CreatedAt = DateTimeOffset.UtcNow;
        IsDeleted = false;
        Title = rating.Title;
        Rating = (Rate)rating.Rate;
        IsDeleted = false;
    }
    public RatingEntity() { }
    
    public enum Rate
    {
        Awful = 1,
        Bad = 2,
        Medium = 3,
        Good = 4,
        Perfect = 5
    }
    public RatingResponse ToRatingResponse()
    {
        return new RatingResponse
        {
            Title = this.Title,
            Description = this.Description ?? string.Empty,
            CreatedBy = this.CreatedBy,
            ModifiedAt = this.ModifiedAt,
            Rating = (int)this.Rating
        };
    }
}