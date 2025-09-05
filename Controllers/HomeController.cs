using Microsoft.AspNetCore.Mvc;
using TestApp.Models;
using TestApp.Services;
using System.Diagnostics;

namespace TestApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPokemonService _pokemonService;

        public HomeController(ILogger<HomeController> logger, IPokemonService pokemonService)
        {
            _logger = logger;
            _pokemonService = pokemonService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetPokemon(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Pokemon name is required");
            }

            try
            {
                var pokemon = await _pokemonService.GetPokemonAsync(name);

                if (pokemon == null)
                {
                    return NotFound($"Pokemon '{name}' not found");
                }

                return Json(pokemon);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting Pokemon: {PokemonName}", name);
                return StatusCode(500, "An error occurred while fetching Pokemon data");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}