namespace OfferService.Models.Exceptions;

public sealed class OfferAlreadyExistException : Exception
{
    public OfferAlreadyExistException() : base("User with this name or email already exists") { }
    public OfferAlreadyExistException(string message) : base(message) { }
}