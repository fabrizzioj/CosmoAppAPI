using CosmoAppAPI.Models;
namespace CosmoAppAPI.Services;

public class AstronomyPictureService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    
    public AstronomyPictureService(HttpClient httpClient,  IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["NasaApi:ApiKey"];
    }

    public async Task<AstronomyPicture?> GetTodayPictureAsync()
    {
        var today = DateTime.Today.ToString("yyyy-MM-dd");
        return await GetPictureByDateAsync(today);
    }
    
    public async Task<AstronomyPicture> GetPictureByDateAsync(string date)
    {
        var url = $"https://api.nasa.gov/planetary/apod?api_key={_apiKey}&date={date}";
        return await _httpClient.GetFromJsonAsync<AstronomyPicture>(url);
    }
}