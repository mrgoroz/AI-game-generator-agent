using System.Threading.Tasks;

namespace GameIdeaService.Domain
{
    public interface IGameIdeaGenerator
    {
        Task<GameIdea> GenerateIdeaFromTrendAsync(string trend);
    }
}
