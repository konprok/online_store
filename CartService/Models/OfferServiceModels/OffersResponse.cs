namespace CartService.Models.OfferServiceModels;

public record OffersResponse
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public string? Description { get; set; }
    public Uri? Logo { get; set; }
    public double Price { get; set; }
}