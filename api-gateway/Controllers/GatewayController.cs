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
            // Note: In a real microservices environment, this URL would come from config or service discovery
            var response = await _httpClient.GetStringAsync("http://localhost:5112/Trends");
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling Google Trends Service");
            return StatusCode(500, $"Error calling Google Trends Service: {ex.Message}");
        }
    }
}
