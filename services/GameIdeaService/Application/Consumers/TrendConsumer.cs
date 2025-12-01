using System.Threading.Tasks;
using GameIdeaService.Domain.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace GameIdeaService.Application.Consumers
{
    public class TrendConsumer : IConsumer<TrendDiscovered>
    {
        private readonly GameIdeaManager _manager;
        private readonly ILogger<TrendConsumer> _logger;

        public TrendConsumer(GameIdeaManager manager, ILogger<TrendConsumer> logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<TrendDiscovered> context)
        {
            _logger.LogInformation("Received trend: {TrendName}", context.Message.TrendName);
            
            try
            {
                var idea = await _manager.CreateGameIdeaForTrendAsync(context.Message.TrendName);
                _logger.LogInformation("Created game idea: {Title}", idea.Title);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error creating game idea for trend: {TrendName}", context.Message.TrendName);
                throw;
            }
        }
    }
}
