using Microsoft.AspNetCore.Mvc;
using CosmoAppAPI.Services;
using CosmoAppAPI.Models;
using CosmoAppAPI.DTOs;

namespace CosmoAppAPI.Controllers;

[ApiController]
[Route("apod")]
public class AstronomyPictureController : ControllerBase
{
    private readonly AstronomyPictureService _pictureService;
    
    public AstronomyPictureController(AstronomyPictureService pictureService)
    {
        _pictureService = pictureService;
    }
    
    [HttpGet("today", Name = "GetTodayPicture")]
    public async Task<ActionResult<AstronomyPicture>> Get()
    {
        try
        {
            var picture = await _pictureService.GetTodayPictureAsync();
            
            if (picture == null) return NotFound();
            
            return Ok(picture); 
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while fetching today's astronomy picture.");
        }
    }
    
    [HttpGet("photos/{date}", Name = "GetPictureByDate")]
    public async Task<ActionResult<AstronomyPictureDto>> Get(DateTime date)
    {
        try
        {
            var formattedDate = date.ToString("yyyy-MM-dd");
            var picture = await _pictureService.GetPictureByDateAsync(formattedDate);
            
            if (picture == null) return NotFound();

            var trimmedResponse = new AstronomyPictureDto
            {
                Title = picture.Title,
                Url = picture.Url,
                Explanation = picture.Explanation,
                Date = picture.Date.ToString("yyyy-MM-dd")
            };

            return Ok(trimmedResponse); 
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while fetching the picture by date.");
        }
    }
}