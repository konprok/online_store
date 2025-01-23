using Microsoft.AspNetCore.Mvc;
using CartService.Database.Entities;
using CartService.Models;
using CartService.Models.OfferServiceModels;
using CartService.Services.Interfaces;

namespace CartService.Controllers;

[ApiController]
[Route("cart")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<CartResponse>> GetCart(Guid userId)
    {
        try
        {
            return Ok(await _cartService.GetCart(userId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
    
    [HttpGet("items/{userId:guid}")]
    public async Task<ActionResult<int>> CountItamsInCart(Guid userId)
    {
        try
        {
            return Ok(await _cartService.CountItemsInCart(userId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
    
    
    [HttpPost("{userId:guid}")]
    public async Task<ActionResult<CartEntity>> CreateCart(Guid userId)
    {
        try
        {
            return Ok(await _cartService.CreateCart(userId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
    
    [HttpPatch("{userId:guid}")]
    public async Task<ActionResult<bool>> AddToCart(Guid userId, long offerId)
    {
        try
        {
            return Ok(await _cartService.AddToCart(userId, offerId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpDelete("{userId:guid}")]
    public async Task<ActionResult<bool>> RemoveFromCart(Guid userId, long offerId)
    {
        try
        {
            return Ok(await _cartService.RemoveFromCart(userId, offerId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}