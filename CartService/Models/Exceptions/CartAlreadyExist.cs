namespace CartService.Models.Exceptions;

public sealed class CartAlreadyExist : Exception
{
    public CartAlreadyExist() : base("This user already has a cart.") { }
    public CartAlreadyExist(string message) : base(message) { }
}