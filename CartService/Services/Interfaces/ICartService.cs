using CartService.Database.Entities;
using CartService.Models;
using CartService.Models.OfferServiceModels;

namespace CartService.Services.Interfaces;

public interface ICartService
{
    Task<bool> AddToCart(Guid userId, long offerId);
    Task<CartResponse> GetCart(Guid userId);
    Task<int> CountItemsInCart(Guid userId);
    Task<CartEntity> CreateCart(Guid userId);
    Task<bool> RemoveFromCart(Guid userId, long offerId);

}