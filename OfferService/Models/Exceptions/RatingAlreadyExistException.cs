namespace OfferService.Models.Exceptions;

public class RatingAlreadyExistException : Exception
{
    public RatingAlreadyExistException() : base("Rating already exist") { }
    public RatingAlreadyExistException(string message) : base(message) { }
}
