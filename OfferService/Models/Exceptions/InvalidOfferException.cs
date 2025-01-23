namespace OfferService.Models.Exceptions;

public sealed class InvalidOfferException : Exception
{
    public InvalidOfferException() : base("Invalid user.") { }
    public InvalidOfferException(string message) : base(message) { }
}