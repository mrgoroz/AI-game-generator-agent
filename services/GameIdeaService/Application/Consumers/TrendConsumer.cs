using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using SharedKernel.Events;
using GameIdeaService.Domain;

namespace GameIdeaService.Application.Consumers
{
    public class TrendConsumer : IConsumer<ITrendDiscovered>
    {
        private readonly GameIdeaManager _manager;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<TrendConsumer> _logger;

        public TrendConsumer(GameIdeaManager manager, IPublishEndpoint publishEndpoint, ILogger<TrendConsumer> logger)
        {
            _manager = manager;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ITrendDiscovered> context)
        {
            _logger.LogInformation("Received trend: {TrendName}", context.Message.TrendName);

            try
            {
                var idea = await _manager.CreateGameIdeaForTrendAsync(context.Message.TrendName);
                _logger.LogInformation("Created game idea: {Title}", idea.Title);

                await _publishEndpoint.Publish<IGameIdeaGenerated>(new
                {
                    TrendName = context.Message.TrendName,
                    GameTitle = idea.Title,
                    GameDescription = idea.Description,
                    Genre = idea.Genre,
                    GameId = idea.Id
                });
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error creating game idea for trend: {TrendName}", context.Message.TrendName);
                throw;
            }
        }
    }
}
