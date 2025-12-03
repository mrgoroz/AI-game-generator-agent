using Microsoft.AspNetCore.Mvc;
using MassTransit;
using GoogleTrendsService.Services;
using SharedKernel.Events;

namespace GoogleTrendsService.Controllers;

[ApiController]
[Route("[controller]")]
public class TrendsController : ControllerBase
{
    private readonly ITrendsService _trendsService;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<TrendsController> _logger;

    public TrendsController(
        ITrendsService trendsService,
        IPublishEndpoint publishEndpoint,
        ILogger<TrendsController> logger)
    {
        _trendsService = trendsService;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    [HttpPost("fetch")]
    public async Task<IActionResult> FetchAndPublishTrends()
    {
        try
        {
            var trends = await _trendsService.FetchTrendingTopicsAsync();

            foreach (var trend in trends)
            {
                _logger.LogInformation("Publishing trend: {TrendName}", trend);

                await _publishEndpoint.Publish<ITrendDiscovered>(new
                {
                    TrendName = trend
                });
            }

            return Ok(new { Message = $"Published {trends.Count} trends", Trends = trends });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching trends");
            return StatusCode(500, "Internal Server Error");
        }
    }
}
