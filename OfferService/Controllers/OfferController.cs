using Microsoft.AspNetCore.Mvc;
using OfferService.Database.Entities;
using OfferService.Models;
using OfferService.Services.Interfaces;

namespace OfferService.Controllers;

[ApiController]
//[Route("offer")]
public sealed class OfferController : ControllerBase
{
    private readonly IOfferService _offerService;
    public OfferController(IOfferService offerService)
    {
        _offerService = offerService;
    }
    
    [HttpPost("/users/{userId:guid}/offers")]
    public async Task<ActionResult<OfferEntity>> PostOffer(Guid userId, [FromBody] Offer offer)
    {
        try
        {
            return Ok(await _offerService.PostOffer(userId, offer));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
    
    [HttpGet("/users/{userId:guid}/offer")]
    public async Task<ActionResult<IEnumerable<OfferResponse>>> GetUsersOffers(Guid userId)
    {
        try
        {
            return Ok(await _offerService.GetUsersOffers(userId));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
    
    //CO TO ZA GÓWNO XD
    [HttpPost("/offers")]
    public async Task<ActionResult<List<OfferResponse>>> GetOffersByIds([FromBody] List<long> offerIds)
    {
        try
        {
             return Ok(await _offerService.GetOffersByIds(offerIds));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
    
    [HttpGet("/offers")]
    public async Task<ActionResult<OfferResponse>> GetOffers()
    {
        try
        {
            return Ok(await _offerService.GetOffers());
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
    
    [HttpGet("/offers/{offerId:long}")]
    public async Task<ActionResult<OfferResponse>> GetOffer(long offerId)
    {
        try
        {
            return Ok(await _offerService.GetOffer(offerId));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
    
    [HttpPatch("/users/{userId:guid}/offer/{offerId:long}")]
    public async Task<ActionResult<OfferEntity>> PatchOffer(Guid userId, long offerId, [FromBody] Offer offer)
    {
        try
        {
            return Ok(await _offerService.PatchOffer(userId, offerId, offer));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
    
    [HttpDelete("/users/{userId:guid}/offer/{offerId:long}")]
    public async Task<ActionResult<bool>> DeleteOffer(Guid userId, long offerId)
    {
        try
        {
            return Ok(await _offerService.DeleteOffer(userId, offerId));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
    
    [HttpGet("/category/{categoryId}/offers")]
    public async Task<ActionResult<OfferResponse>> GetOfferByCategory(int categoryId)
    {
        try
        {
            return Ok(await _offerService.GetOfferByCategory(categoryId));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
    
    [HttpDelete("/users/{userId:guid}")]
    public async Task<ActionResult<bool>> DeleteUserOffers(Guid userId)
    {
        try
        {
            return Ok(await _offerService.DeleteOffers(userId));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
    
}