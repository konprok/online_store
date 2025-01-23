namespace OfferService.Models.Exceptions;

public sealed class OfferDoesNotExistException : Exception
{
    public OfferDoesNotExistException() : base("Offer with this id does not exist") { }
    public OfferDoesNotExistException(string message) : base(message) { }
}