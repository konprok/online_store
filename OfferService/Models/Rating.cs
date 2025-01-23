using OfferService.Database.Entities;

namespace OfferService.Models;

public sealed class Rating
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid CreatedBy { get; set;}
    public int Rate { get; set; }
}