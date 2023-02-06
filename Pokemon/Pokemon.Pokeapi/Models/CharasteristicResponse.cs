/// <summary>
/// Class to represent the response from the endpoint https://pokeapi.co/api/v2/pokemon
/// Class generated automatically with the tool: https://app.quicktype.io/?l=csharp
/// </summary>
using Newtonsoft.Json;

namespace Pokemon.Pokeapi.Models
{
    public partial class CharasteristicResponse
    {
        [JsonProperty("descriptions")]
        public List<Description> Descriptions { get; set; }

        [JsonProperty("gene_modulo")]
        public long GeneModulo { get; set; }

        [JsonProperty("highest_stat")]
        public HighestStat HighestStat { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("possible_values")]
        public List<long> PossibleValues { get; set; }
    }

    public partial class Description
    {
        [JsonProperty("description")]
        public string DescriptionDescription { get; set; }

        [JsonProperty("language")]
        public HighestStat Language { get; set; }
    }

    public partial class HighestStat
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }
}
