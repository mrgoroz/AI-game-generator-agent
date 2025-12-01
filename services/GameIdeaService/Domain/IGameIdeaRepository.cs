using System.Threading.Tasks;

namespace GameIdeaService.Domain
{
    public interface IGameIdeaRepository
    {
        Task SaveGameIdeaAsync(GameIdea gameIdea);
        Task<GameIdea?> GetGameIdeaAsync(string id);
    }
}
