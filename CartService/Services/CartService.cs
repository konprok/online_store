using CartService.ApiReferences;
using CartService.Database.Entities;
using CartService.Database.Repositories.Interfaces;
using CartService.Models;
using CartService.Models.OfferServiceModels;
using CartService.Services.Interfaces;

namespace CartService.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IOfferApiReference _offerApiReference;

    public CartService(ICartRepository cartRepository, IOfferApiReference offerApiReference)
    {
        _cartRepository = cartRepository;
        _offerApiReference = offerApiReference;
    }

    public async Task<CartResponse> GetCart(Guid userId)
    {
        var cartEntity = await _cartRepository.GetCartByUserId(userId);
        if (cartEntity.Offers != null)
        {
            var offers = await _offerApiReference.GetOffersByIds(cartEntity.Offers);
            CartResponse cartResponse = new CartResponse(offers);
            return cartResponse;
        }

        return new CartResponse();
    }

    public async Task<int> CountItemsInCart(Guid userId)
    {
        var cart = await _cartRepository.GetCartByUserId(userId);
        var numberOfItemsInCart = cart.Offers?.Count();
        if (numberOfItemsInCart == null)
        {
            return 0;
        }
        
        return (int)numberOfItemsInCart;
    }
    
    public async Task<bool> AddToCart(Guid userId, long offerId)
    {
        var cart = await _cartRepository.GetCartByUserId(userId);
        cart.Offers?.Add(offerId);
        await _cartRepository.SaveAsync();
        return true;
    }
   
    public async Task<CartEntity> CreateCart(Guid userId)
    {
        CartEntity cartEntity = new CartEntity(userId);
        await _cartRepository.InsertCartAsync(cartEntity);
        await _cartRepository.SaveAsync();
        return cartEntity;
    }
    
    public async Task<bool> RemoveFromCart(Guid userId, long offerId)
    {
        var cart = await _cartRepository.GetCartByUserId(userId);
        cart.Offers?.Remove(offerId);
        await _cartRepository.SaveAsync();
        return true;
    }
}