namespace CartService.Database.Entities;

public class CartEntity
{
    public long Id { get; set; }
    public Guid UserId { get; set; }
    public List<long>? Offers { get; set; }

    public CartEntity(Guid userId)
    {
        UserId = userId;
        Id = new long();
        Offers = new List<long>();
    }

    public CartEntity()
    {
        
    }
}