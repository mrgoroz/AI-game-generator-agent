using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using GameIdeaService.Domain;
using Microsoft.Extensions.Configuration;

namespace GameIdeaService.Infrastructure
{
    public class GroqGameIdeaGenerator : IGameIdeaGenerator
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _model;

        public GroqGameIdeaGenerator(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["Groq:ApiKey"] ?? throw new ArgumentNullException("Groq:ApiKey is missing");
            _model = configuration["Groq:Model"] ?? "llama3-8b-8192";
        }

        public async Task<GameIdea> GenerateIdeaFromTrendAsync(string trend)
        {
            var prompt = $@"Generate a simple, funny, and addictive video game idea based on the trend: '{trend}'. 
            Return ONLY a JSON object with the following properties: title, description, genre, platform. 
            Do not include any markdown formatting or extra text.";

            var requestBody = new
            {
                messages = new[]
                {
                    new { role = "user", content = prompt }
                },
                model = _model
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.groq.com/openai/v1/chat/completions");
            request.Headers.Add("Authorization", $"Bearer {_apiKey}");
            request.Content = JsonContent.Create(requestBody);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadFromJsonAsync<JsonElement>();
            var content = responseJson.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();

            // Clean up potential markdown code blocks if the LLM ignores instructions
            content = content.Replace("```json", "").Replace("```", "").Trim();

            var ideaData = JsonSerializer.Deserialize<GameIdeaData>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return new GameIdea
            {
                Title = ideaData.Title,
                Description = ideaData.Description,
                Genre = ideaData.Genre,
                Platform = ideaData.Platform,
                TrendTopic = trend
            };
        }

        private class GameIdeaData
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string Genre { get; set; }
            public string Platform { get; set; }
        }
    }
}
