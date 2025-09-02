using System.Text;
using System.Text.Json;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http;

public class HttpCommandDataClient : ICommandDataClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public HttpCommandDataClient(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }
    public async Task SendPlatformToCommand(PlatformReadDto plat)
    {
        var httpContent = new StringContent(JsonSerializer.Serialize(plat),
            Encoding.UTF8,
            "application/json");
        
        var response = await _httpClient.PostAsync($"{_config["CommandService"]}/api/c/Platforms/", 
            httpContent);
        
        if (!response.IsSuccessStatusCode) Console.WriteLine(" --> Sync Post To Command Platform Failed --");
     
        Console.WriteLine(" --> Sync Post To Command Platform Success");
    }
}