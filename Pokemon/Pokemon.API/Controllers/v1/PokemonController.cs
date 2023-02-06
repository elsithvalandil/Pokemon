using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Pokemon.Application.Features.Pokemon.Querys.GetPokemon;
using Pokemon.Domain;

namespace Pokemon.API.Controllers.v1
{
    /// <summary>
    /// Pokemon controller version 1
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IMediator _mediator;
        private const String pokeInfoCacheKey = "_pokeInfo";
        private readonly IMemoryCache _cache;
        private readonly ILogger<PokemonController> _logger;

        public PokemonController(IMediator Mediator, IMemoryCache Cache, ILogger<PokemonController> Logger)
        {
            _mediator = Mediator;
            _cache = Cache;
            _logger = Logger;
        }

        #region Methods
        /// <summary>
        /// Method to obtain the information of the pokemon by  name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}", Name = "GetPokemon")]
        [ProducesResponseType(typeof(PokemonInfo), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PokemonInfo>> GetPokemonByName(string name)
        {
            PokemonInfo pokemon;

            if (_cache.TryGetValue($"{pokeInfoCacheKey}_{name}", out pokemon))
            {
                _logger.LogInformation("Pokemon information found in cache");
            }
            else
            {
                var query = new GetPokemonQuery(name);
                pokemon = await _mediator.Send(query);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                                        .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                                        .SetPriority(CacheItemPriority.Normal)
                                        .SetSize(1024);
                _cache.Set($"{pokeInfoCacheKey}_{name}", pokemon, cacheEntryOptions);

            }

            return pokemon;
        }
        #endregion
    }
}
