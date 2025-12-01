using System.Threading.Tasks;
using GameIdeaService.Application;
using GameIdeaService.Domain;
using Microsoft.AspNetCore.Mvc;

namespace GameIdeaService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameIdeaController : ControllerBase
    {
        private readonly GameIdeaManager _manager;
        private readonly IGameIdeaRepository _repository;

        public GameIdeaController(GameIdeaManager manager, IGameIdeaRepository repository)
        {
            _manager = manager;
            _repository = repository;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateGameIdea([FromBody] string trend)
        {
            if (string.IsNullOrWhiteSpace(trend))
                return BadRequest("Trend cannot be empty");

            var idea = await _manager.CreateGameIdeaForTrendAsync(trend);
            return Ok(idea);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameIdea(string id)
        {
            var idea = await _repository.GetGameIdeaAsync(id);
            if (idea == null)
                return NotFound();

            return Ok(idea);
        }
    }
}
