namespace OfferService.Models.Exceptions;

public class InvalidRatingException : Exception
{
    public InvalidRatingException() : base("Invalid rating.") { }
    public InvalidRatingException(string message) : base(message) { }   
}