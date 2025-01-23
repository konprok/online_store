using Microsoft.AspNetCore.Mvc;
using OfferService.Database.Entities;
using OfferService.Models;
using OfferService.Models.Exceptions;
using OfferService.Services.Interfaces;

namespace OfferService.Controllers;

[ApiController]
//[Route("rating")]
public class RatingController : ControllerBase
{
    private readonly IRatingService _ratingService;

    public RatingController(IRatingService ratingService)
    {
        _ratingService = ratingService;
    }

    [HttpPost("ratings/{offerId:long}")]
    public async Task<ActionResult<RatingResponse>> PostRating(long offerId, [FromBody] Rating rating)
    {
        try
        {
            return Ok(await _ratingService.PostRating(offerId, rating));
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (OfferDoesNotExistException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidRatingException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpGet("ratings/{offerId:long}")]
    public async Task<ActionResult<RatingResponse>> PostRating(long offerId)
    {
        try
        {
            return Ok(await _ratingService.GetRatingsByOfferId(offerId));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
    
}