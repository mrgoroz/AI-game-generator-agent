using System.Threading.Tasks;
using GameIdeaService.Domain;

namespace GameIdeaService.Application
{
    public class GameIdeaManager
    {
        private readonly IGameIdeaGenerator _generator;
        private readonly IGameIdeaRepository _repository;

        public GameIdeaManager(IGameIdeaGenerator generator, IGameIdeaRepository repository)
        {
            _generator = generator;
            _repository = repository;
        }

        public async Task<GameIdea> CreateGameIdeaForTrendAsync(string trend)
        {
            var gameIdea = await _generator.GenerateIdeaFromTrendAsync(trend);
            await _repository.SaveGameIdeaAsync(gameIdea);
            return gameIdea;
        }
    }
}
