using Microsoft.AspNetCore.Mvc;
using CosmoAppAPI.Services;

namespace CosmoAppAPI.Controllers;

[ApiController]
[Route("apod/today")]
public class AstronomyPictureController : ControllerBase
{
    private readonly AstronomyPictureService _pictureService;
    
    public AstronomyPictureController(AstronomyPictureService pictureService)
    {
        _pictureService = pictureService;
    }
    
    [HttpGet(Name = "GetAstronomyPicture")]
    public async Task<ActionResult<AstronomyPicture>> Get()
    {
        try
        {
            var picture = await _pictureService.GetTodayPictureAsync();
            if (picture == null) 
                return NotFound();
            return Ok(picture); 
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while fetching the astronomy picture.");
        }
    }
}