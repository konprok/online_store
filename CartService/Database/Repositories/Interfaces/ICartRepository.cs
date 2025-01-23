using CartService.Database.Entities;

namespace CartService.Database.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<CartEntity> GetCartByUserId(Guid userId);
        Task InsertCartAsync(CartEntity cartEntity);
        Task SaveAsync();
    }
}
