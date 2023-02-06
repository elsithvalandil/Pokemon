using MediatR;
using Pokemon.Domain;

namespace Pokemon.Application.Features.Pokemon.Querys.GetPokemon
{
    /// <summary>
    /// Class that represents the functionality to retrieve pokemon information by name.
    /// Implements MediatR for communication with its respective Handler.
    /// </summary>
    public class GetPokemonQuery : IRequest<PokemonInfo>
    {
        public String _Name { get; set; } = String.Empty;

        /// <summary>
        /// Constructor with the name parameter to search.
        /// </summary>
        /// <param name="Name"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public GetPokemonQuery(String Name)
        {
            _Name = Name ?? throw new ArgumentNullException(nameof(Name));
        }
    }
}
