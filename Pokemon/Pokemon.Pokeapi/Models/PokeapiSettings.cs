/// <summary>
/// Class to represent the PokeapiSettings from the appsettings.json file
/// </summary>
namespace Pokemon.Pokeapi.Models
{
    public class PokeapiSettings
    {
        public String UrlPokemon {  get; set; }
        public String UrlCharasteristic { get; set;}
        public String Lang { get; set; }
    }
}
