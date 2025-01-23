namespace CartService.Models.Exceptions;

public class CartNotFound : Exception
{
    public CartNotFound() : base("Cart not found.") { }
    public CartNotFound(string message) : base(message) { }
}