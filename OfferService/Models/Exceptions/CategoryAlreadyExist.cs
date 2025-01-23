namespace OfferService.Models.Exceptions;

public sealed class CategoryAlreadyExist : Exception
{
    public CategoryAlreadyExist() : base("This category already exist") { }
    public CategoryAlreadyExist(string message) : base(message) { }
}