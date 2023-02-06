using MediatR;
using Microsoft.Extensions.Logging;
using Pokemon.Application.Contracts.Infraestructure;
using Pokemon.Application.Exceptions;
using Pokemon.Domain;

namespace Pokemon.Application.Features.Pokemon.Querys.GetPokemon
{
    /// <summary>
    /// Class for handle functionality of GetPokemonQuery.
    /// </summary>
    internal class GetPokemonQueryHandler : IRequestHandler<GetPokemonQuery, PokemonInfo>
    {
        private readonly IPokeapiService _pokeapiService;
        private readonly ILogger<GetPokemonQueryHandler> _logger;

        /// <summary>
        /// Constructor with 
        /// </summary>
        /// <param name="PokeapiService"></param>
        /// <param name="Logger"></param>
        public GetPokemonQueryHandler(IPokeapiService PokeapiService, ILogger<GetPokemonQueryHandler> Logger)
        {
            _pokeapiService = PokeapiService;
            _logger = Logger;
        }

        /// <summary>
        /// Implementation of the handle method of the Mediatr interface, contains the logic to obtain the information of the pokemon. 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<PokemonInfo> Handle(GetPokemonQuery request, CancellationToken cancellationToken)
        {

            var pokemonInfo = await _pokeapiService.GetPokemonByName(request._Name);
            if(pokemonInfo != null)
            {
                return pokemonInfo;
            }
            else
            {
                _logger.LogError($"Pokemon Not Found {request._Name}");
                throw new NotFoundException(nameof(PokemonInfo), request._Name);
                
            }
            
        }
    }
}
