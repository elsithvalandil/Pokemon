using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Pokemon.Application.Contracts.Infraestructure;
using Pokemon.Domain;
using Pokemon.Pokeapi.Models;
using RestSharp;
using System.Diagnostics.Metrics;
using System.Net;
using System.Text;

namespace Pokemon.Pokeapi.Services
{
    /// <summary>
    /// Concrete class that implements the interface IPokeapiService
    /// </summary>
    public class PokeapiService : IPokeapiService
    {
        #region PrivateAttributes
        private readonly PokeapiSettings _pokeSettings;
        private readonly ILogger<PokeapiService> _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor of the class with dependency injection.
        /// </summary>
        /// <param name="pokeSettings"></param>
        /// <param name="logger"></param>
        public PokeapiService(IOptions<PokeapiSettings> pokeSettings, ILogger<PokeapiService> logger)
        {
            _pokeSettings = pokeSettings.Value;
            _logger = logger;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Implementation of the method that consumes the pokeapi endpoints to obtain name, type and characteristic.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns>Object PokemonInfo with the required pokemon information.
        /// If not exists the Pokemon return null.
        /// </returns>
        public async Task<PokemonInfo> GetPokemonByName(String Name)
        {
            PokemonInfo pokemon = new PokemonInfo();

            var clientPokemon = new RestClient(_pokeSettings.UrlPokemon);
            var requestPokemon = new RestRequest(Method.GET);

            requestPokemon.AddUrlSegment("name", Name);
            try
            {
                IRestResponse responsePokemon = await clientPokemon.ExecuteAsync(requestPokemon);
                if (responsePokemon.StatusCode == HttpStatusCode.OK)
                {
                    PokemonResponse pokemonResponse = JsonConvert.DeserializeObject<PokemonResponse>(responsePokemon.Content);
                    if (pokemonResponse != null)
                    {
                        pokemon.Name = pokemonResponse.Name;

                        StringBuilder types = new StringBuilder();
                        String delimiter = String.Empty;
                        foreach (TypeElement type in pokemonResponse.Types)
                        {
                            types.Append(delimiter);
                            types.Append(type.Type.Name);
                            delimiter = " - ";
                        }
                        pokemon.Type = types.ToString(); 

                        var clientCharacteristic = new RestClient(_pokeSettings.UrlCharasteristic);
                        var requestCharacteristic = new RestRequest(Method.GET);

                        requestCharacteristic.AddUrlSegment("id", pokemonResponse.Id);
                        try
                        {
                            IRestResponse responseCharasteristic = await clientCharacteristic.ExecuteAsync(requestCharacteristic);
                            _logger.LogInformation(responseCharasteristic.StatusCode.ToString());
                            if (responseCharasteristic.StatusCode == HttpStatusCode.OK)
                            {
                                CharasteristicResponse charasteristicResponse = JsonConvert.DeserializeObject<CharasteristicResponse>(responseCharasteristic.Content);
                                if (charasteristicResponse != null)
                                {
                                    pokemon.Description = charasteristicResponse.Descriptions.Where(d => d.Language.Name.Equals(_pokeSettings.Lang)).FirstOrDefault().DescriptionDescription;
                                }
                            }
                        }
                        catch (Exception exc)
                        {
                            _logger.LogError($"Error To Request Charasteristic pokemon {Name}\n {exc.Message}");
                            pokemon.Description = "Can't retrieve characteristic from Pokeapi";
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exp)
            {
                _logger.LogError($"Error To Request pokemon {Name}\n {exp.Message}");
                return null;
            }
            
            return pokemon;
        }
        #endregion
    }
}
