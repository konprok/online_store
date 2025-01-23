namespace OfferService.Models;

public class RatingResponse
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Username { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTimeOffset? ModifiedAt { get; set; }
    public int Rating { get; set; }
}