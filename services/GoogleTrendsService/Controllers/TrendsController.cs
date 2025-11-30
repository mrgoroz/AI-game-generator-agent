using Microsoft.AspNetCore.Mvc;

namespace GoogleTrendsService.Controllers;

[ApiController]
[Route("[controller]")]
public class TrendsController : ControllerBase
{
    private static readonly string[] MockTrends = new[]
    {
        "AI Agents", "Microservices", "DotNet 9", "Google Gemini", "Antigravity"
    };

    private readonly ILogger<TrendsController> _logger;

    public TrendsController(ILogger<TrendsController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetTrends")]
    public IEnumerable<string> Get()
    {
        _logger.LogInformation("Fetching trends...");
        return MockTrends;
    }
}
