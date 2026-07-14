namespace CosmoAppAPI.Services;

public class AstronomyPictureService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    
    public AstronomyPictureService(HttpClient httpClient,  IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<AstronomyPicture> GetTodayPictureAsync()
    {
        var apiKey = _configuration["NasaApi:ApiKey"];
        var today = DateTime.Today.ToString("yyyy-MM-dd");
        var baseUrl = $"https://api.nasa.gov/planetary/apod?api_key={apiKey}";
        
        var url = $"{baseUrl}&date={today}";
        
        return await _httpClient.GetFromJsonAsync<AstronomyPicture>(url);
    }
}