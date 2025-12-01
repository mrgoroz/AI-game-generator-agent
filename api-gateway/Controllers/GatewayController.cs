using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class GatewayController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<GatewayController> _logger;

    public GatewayController(HttpClient httpClient, ILogger<GatewayController> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    [HttpGet("trends")]
    public async Task<IActionResult> GetTrends()
    {
        try
        {
            _logger.LogInformation("Calling Google Trends Service...");
            // Assuming GoogleTrendsService is running on localhost:5112
            var response = await _httpClient.GetStringAsync("http://localhost:5112/Trends");
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling Google Trends Service");
            return StatusCode(500, $"Error calling Google Trends Service: {ex.Message}");
        }
    }

    [HttpPost("game-ideas/generate")]
    public async Task<IActionResult> GenerateGameIdea([FromBody] string trend)
    {
        try
        {
            _logger.LogInformation("Calling Game Idea Service to generate idea for trend: {Trend}", trend);
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5297/api/GameIdea/generate", trend);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling Game Idea Service");
            return StatusCode(500, $"Error calling Game Idea Service: {ex.Message}");
        }
    }

    [HttpGet("game-ideas/{id}")]
    public async Task<IActionResult> GetGameIdea(string id)
    {
        try
        {
            _logger.LogInformation("Calling Game Idea Service to get idea: {Id}", id);
            var response = await _httpClient.GetStringAsync($"http://localhost:5297/api/GameIdea/{id}");
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling Game Idea Service");
            return StatusCode(500, $"Error calling Game Idea Service: {ex.Message}");
        }
    }
}
