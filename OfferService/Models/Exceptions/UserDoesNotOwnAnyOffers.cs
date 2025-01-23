namespace OfferService.Models.Exceptions;

public class UserDoesNotOwnAnyOffers : Exception
{
    public UserDoesNotOwnAnyOffers() : base("User does not own any offers") { }
    public UserDoesNotOwnAnyOffers(string message) : base(message) { }
}