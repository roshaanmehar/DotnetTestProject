using TestApp.Models;
using System.Text.Json;
using System.Threading.Tasks;
using System;

namespace TestApp.Services
{
    public interface IPokemonService
    {
        Task<Pokemon?> GetPokemonAsync(string name);
    }

    public class PokemonService : IPokemonService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PokemonService> _logger;
        private const string POKEAPI_BASE_URL = "https://pokeapi.co/api/v2/pokemon/";

        public PokemonService(HttpClient httpClient, ILogger<PokemonService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<Pokemon?> GetPokemonAsync(string name)
        {
            try
            {
                _logger.LogInformation("Fetching Pokemon data for: {PokemonName}", name);

                var response = await _httpClient.GetAsync($"{POKEAPI_BASE_URL}{name.ToLower()}");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Pokemon not found: {PokemonName}", name);
                    return null;
                }

                var jsonContent = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var pokemon = JsonSerializer.Deserialize<Pokemon>(jsonContent, options);

                _logger.LogInformation("Successfully fetched Pokemon: {PokemonName}", pokemon?.Name);

                return pokemon;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching Pokemon: {PokemonName}", name);
                return null;
            }
        }
    }
}