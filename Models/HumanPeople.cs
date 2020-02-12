using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAPICapco.Models
{
    public class HumanPeople
    {
        //Nome
        [JsonPropertyName("name")]
        public string Name { get; set; }

        //Peso
        [JsonPropertyName("mass")]
        public string Mass { get; set; }

        [JsonPropertyName("species")]
        public List<string> Species { get; set; }

        [JsonPropertyName("url")]
        public string PersonURL { get; set; }

    }
}