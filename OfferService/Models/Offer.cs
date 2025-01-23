namespace OfferService.Models;

public sealed class Offer
{
    public string Title { get; set; }
    public int Category { get; set; }
    public string? Description { get; set; }
    public Uri? Logo { get; set; }
    public double Price { get; set; }
}