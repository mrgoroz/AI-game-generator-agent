using System.Threading.Tasks;
using GameIdeaService.Domain;
using GameIdeaService.Infrastructure.Models;
using Supabase;

namespace GameIdeaService.Infrastructure
{
    public class SupabaseGameIdeaRepository : IGameIdeaRepository
    {
        private readonly Client _client;

        public SupabaseGameIdeaRepository(Client client)
        {
            _client = client;
        }

        public async Task SaveGameIdeaAsync(GameIdea gameIdea)
        {
            var model = new SupabaseGameIdea
            {
                Id = gameIdea.Id,
                Title = gameIdea.Title,
                Description = gameIdea.Description,
                Genre = gameIdea.Genre,
                Platform = gameIdea.Platform,
                TrendTopic = gameIdea.TrendTopic,
                CreatedAt = gameIdea.CreatedAt
            };

            await _client.From<SupabaseGameIdea>().Insert(model);
        }

        public async Task<GameIdea?> GetGameIdeaAsync(string id)
        {
            var response = await _client.From<SupabaseGameIdea>()
                .Where(x => x.Id == id)
                .Single();

            if (response == null) return null;

            return new GameIdea
            {
                Id = response.Id,
                Title = response.Title,
                Description = response.Description,
                Genre = response.Genre,
                Platform = response.Platform,
                TrendTopic = response.TrendTopic,
                CreatedAt = response.CreatedAt
            };
        }
    }
}
