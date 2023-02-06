using Pokemon.Domain;
namespace Pokemon.Application.Contracts.Infraestructure
{
    public interface IPokeapiService
    {
        public Task<PokemonInfo> GetPokemonByName(String Name);
    }
}
