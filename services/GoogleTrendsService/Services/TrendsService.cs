using SharedKernel.Events;

namespace GoogleTrendsService.Services;

public interface ITrendsService
{
    Task<List<string>> FetchTrendingTopicsAsync();
}

public class TrendsService : ITrendsService
{
    private readonly ILogger<TrendsService> _logger;

    public TrendsService(ILogger<TrendsService> logger)
    {
        _logger = logger;
    }

    public async Task<List<string>> FetchTrendingTopicsAsync()
    {
        _logger.LogInformation("Fetching trending topics...");

        // Simulate fetching from Google Trends
        // In a real implementation, this would scrape or use an API
        await Task.Delay(500);

        var trends = new List<string>
        {
            "AI Agents",
            "SpaceX Starship",
            "Quantum Computing",
            "Sustainable Energy"
        };

        return trends;
    }
}
